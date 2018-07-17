import { Directive, ElementRef, Renderer, Input, Output, EventEmitter } from '@angular/core';
import { SeatNode, SeatNodeState } from './model';

declare var SVG: any;
declare var $: any;

@Directive({
  selector: '[appSvgMap]'
})
export class SvgMapDirective {
  allReservedSeats: string[];
  userSelectedSeats: string[];

  htmlElement: any;
  svgContent: string;
  svg: any;

  @Output() onSeatClicked: EventEmitter<any> = new EventEmitter();

  constructor(private ele: ElementRef, private renderer: Renderer) { }

  @Input() set appSvgMap(svgContent: string) {
    if (svgContent == null || svgContent.length === 0 || this.svgContent === svgContent) {
      return;
    }
    this.svgContent = svgContent;
    this.createControls();
    this.loadMap();

    this.handleSingleSeatsClicks();
    this.handleUnnumberedSeatsClicks();

    this.updateLayout();
  }

  @Input('allReservedSeats') set allReservedSeatsSource(seats: any) {
    if (seats == null) {
      return;
    }

    this.allReservedSeats = seats;
    this.updateSeatsStates();
  }

  @Input('userSelectedSeats') set userSelectedSeatsSource(seats: any) {
    if (seats == null) {
      return;
    }

    this.userSelectedSeats = seats;
    this.updateSeatsStates();
  }

  /** region init */
  private createControls() {
    this.htmlElement = this.renderer.createElement(this.ele.nativeElement.parentNode, 'div');
    this.htmlElement.id = 'svgMapCanvas';

    this.svg = SVG('svgMapCanvas');
    this.svg.panZoom();
  }

  private loadMap() {
    this.svg.svg(this.svgContent);
    this.updateSeatsStates();
  }

  private resizeContainer(svg) {
    svg.size(this.htmlElement.clientWidth, this.ele.nativeElement.parentNode.clientHeight);
  }

  private updateLayout() {
    this.resizeContainer(this.svg);

    setInterval(() => {
      this.resizeContainer(this.svg);
    }, 500)
  }

  zoomIntoCenterOfScene() {
    const sceneBox = $('*[data-class=scene]')[0].getBoundingClientRect();
    this.svg.zoom(2, new SVG.Point(- sceneBox.width / 2, sceneBox.height / 2));
  }
  /** end region init */

  /** region event handlers */
  private handleSingleSeatsClicks() {
    const allSeats = this.selectAllSingleSeats();

    const me = this;
    allSeats.click(function () {
      const seat = new SeatNode(this);
      me.handleSeatClick(seat);
    })
  }

  private handleUnnumberedSeatsClicks() {
    const allSeats = this.selectAllUnnumberedSeats();
    const me = this;
    allSeats.click(function () {
      const seat = new SeatNode(this);
      me.handleSeatClick(seat);
    })
  }
  /** end region handlers */

  /** region seats management */
  private selectAllSingleSeats(): any {
    return SVG.select('circle[data-class=seat]');
  }

  private selectAllUnnumberedSeats(): any {
    return SVG.select('rect[data-class=unnumbered-seat');
  }

  private selectSeatById(seatId: number): any {
    const query = '*[data-seat-id="' + seatId + '"]';
    return SVG.select(query).first();
  }

  private updateSeatsStates() {
    if (this.svg == null ||
      this.svgContent == null ||
      this.allReservedSeats == null ||
      this.userSelectedSeats == null) {
      return;
    }
    const unnumberedSeats = this.selectAllUnnumberedSeats();
    unnumberedSeats.each(function (i, children) {
      const seatNode = new SeatNode(this);
      seatNode.markAsFree();
    });

    const allSingleSeats: number[] = this.selectAllSingleSeats()
      .members
      .map(x => new SeatNode(x).getSceneSeatId())

    const freeSeats = allSingleSeats
      .filter(x => this.allReservedSeats.indexOf(x.toString()) === -1)
      .filter(x => this.userSelectedSeats.indexOf(x.toString()) === -1)

    const unavailableSeats = this.allReservedSeats
      .filter(x => this.userSelectedSeats.indexOf(x) === -1);

    this.userSelectedSeats.forEach(seatId => {
      const seatElement = this.selectSeatById(Number.parseInt(seatId));
      const seatNode = new SeatNode(seatElement);
      seatNode.markAsSelected();
    });

    unavailableSeats.forEach(seatId => {
      const seatElement = this.selectSeatById(Number.parseInt(seatId));
      const seatNode = new SeatNode(seatElement);
      seatNode.markAsUnavailable();
    });

    freeSeats.forEach(seatId => {
      const seatElement = this.selectSeatById(seatId);
      const seatNode = new SeatNode(seatElement);
      seatNode.markAsFree();
    });

  }

  private handleSeatClick(seat: SeatNode) {
    const state = seat.getSeatNodeState();
    if (state === SeatNodeState.Unavailable) {
      return;
    }
    this.onSeatClicked.emit(seat);
  }

  public getSeatNodeById(seatId: number): SeatNode {
    return new SeatNode(this.selectSeatById(seatId));
  }

  /** end region seats management */
}
