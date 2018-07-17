declare var SVG: any;

export enum SeatNodeState {
  Unavailable,
  Free,
  Selected
}

export enum SeatNodeType {
  Numbered,
  Unnumbered
}

export class SeatNode {
  constructor(private visual: any) { }

  public getSceneSeatId(): number {
    const id = this.visual.attr('data-seat-id') as number;
    return id;
  }

  getPriceZoneId(): number {
    const priceZoneId = this.visual.attr('data-price-zone-id') as number;
    return priceZoneId;
  }

  public markAsSelected() {
    if (this.getSeatType() === SeatNodeType.Unnumbered) {
      return
    }

    this.setSeatNodeState(SeatNodeState.Selected);
    const stokeColor = this.visual.style('fill');
    this.visual.animate(100).style({
      'fill-opacity': 0.3
      , stroke: stokeColor
      , 'stroke-width': 0.3
    });
  }

  public markAsFree() {
    this.setSeatNodeState(SeatNodeState.Free);
    this.visual.animate(100).style({
      'fill-opacity': 1
      , stroke: '#000'
      , 'stroke-width': 0
    });
  }

  public markAsUnavailable() {
    if (this.getSeatType() === SeatNodeType.Unnumbered) {
      return
    }

    this.setSeatNodeState(SeatNodeState.Unavailable);
    this.visual.style({
      'fill-opacity': 0.03
    });
  }

  public getSeatNodeState(): SeatNodeState {
    const state = this.visual.attr('data-seat-state')
    if (state == null || state === SeatNodeState.Unavailable) {
      return SeatNodeState.Unavailable;
    }

    return state === SeatNodeState.Selected ? SeatNodeState.Selected : SeatNodeState.Free;
  }

  public getSeatType(): SeatNodeType {
    if (this.visual.attr('data-class') === 'unnumbered-seat') {
      return SeatNodeType.Unnumbered;
    } else {
      return SeatNodeType.Numbered;
    }
  }

  public setSeatNodeState(state: SeatNodeState) {
    this.visual.attr('data-seat-state', state)
  }
}
