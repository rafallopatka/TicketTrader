import { Component, OnInit, OnDestroy, Input, ViewChild } from '@angular/core';

import { ActivatedRoute } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import { SeatNodeState, SeatNode } from '../../directives/model';
import { Subscription } from 'rxjs/Subscription';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { SceneService } from 'app/services/scene.service';
import { ReservationService, SeatReservationDto } from 'app/services/reservation.service';
import { ClientOrderDto, UserClientDto } from 'app/services/api/app.clients';
import { User } from 'oidc-client';
import { ClientReservationsService } from 'app/services/client-reservations.service';
import { Subject } from 'rxjs/Subject';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { MessageBus } from 'app/messages/message-bus';
import {
  SeatReservedMessage,
  SeatDiscardedMessage,
  SeatDiscardRequest,
  TicketReservationCancelRequest
} from 'app/seating-plan/messages/messages';
import { SvgMapDirective } from 'app/seating-plan/directives/svg-map.directive';


@Component({
  selector: 'app-plan-visualization',
  templateUrl: './plan-visualization.component.html',
  styleUrls: ['./plan-visualization.component.css']
})
export class PlanVisualizationComponent implements OnInit, OnDestroy {
  canceledTicketReservations: Subscription;
  removeReservationRequestSubscription: Subscription;
  userReservationsSubscription: Subscription;

  allReservedSeats: number[];
  userSelectedSeats: number[];
  visualizationObservable: Observable<string>;
  reservedSeatsSubscription: Subscription;

  eventIdSubject: ReplaySubject<number>
  activeOrderSubject: ReplaySubject<ClientOrderDto>;
  currentClientSubject: ReplaySubject<UserClientDto>;
  currentUserSubject: ReplaySubject<User>;

  @Input() set eventId(eventId: number) { this.notifyPropertyChanged(this.eventIdSubject, eventId); }
  @Input() set activeOrder(activeOrder: ClientOrderDto) { this.notifyPropertyChanged(this.activeOrderSubject, activeOrder); }
  @Input() set currentClient(currentClient: UserClientDto) { this.notifyPropertyChanged(this.currentClientSubject, currentClient); }
  @Input() set currentUser(currentUser: User) { this.notifyPropertyChanged(this.currentUserSubject, currentUser); }

  @ViewChild(SvgMapDirective) map: SvgMapDirective;

  constructor(private sceneService: SceneService,
    private reservationService: ReservationService,
    private clientReservationsService: ClientReservationsService,
    private messageBus: MessageBus) {
    this.eventIdSubject = new ReplaySubject<number>();
    this.activeOrderSubject = new ReplaySubject<ClientOrderDto>();
    this.currentClientSubject = new ReplaySubject<UserClientDto>();
    this.currentUserSubject = new ReplaySubject<User>();
  }

  notifyPropertyChanged<T>(subject: ReplaySubject<T>, value: T) {
    if (value == null) {
      return;
    }

    subject.next(value);
  }

  ngOnInit() {
    const sceneObservable = this.eventIdSubject
      .asObservable()
      .switchMap(eventId => this.sceneService.getScene(eventId));

    this.visualizationObservable = sceneObservable
      .map(scene => scene.svg);

    this.reservedSeatsSubscription = this.eventIdSubject
      .asObservable()
      .switchMap(eventId => this.reservationService.getAllReservedSeats(eventId))
      .subscribe(seats => {
        this.allReservedSeats = seats.map(x => x.sceneSeatId);
      });

    this.userReservationsSubscription = this.withOrderContext()
      .switchMap(context => {
        return this.clientReservationsService.getClientReservations(context.eventId, context.client.clientId, context.order.id)
      })
      .subscribe(reservations => {
        this.userSelectedSeats = reservations.map(x => x.sceneSeatId);
      });

    this.removeReservationRequestSubscription = this.messageBus
      .of(SeatDiscardedMessage)
      .subscribe(message => {
        message.sceneSeatIds.forEach(sceneSeatId => {
          const seatNode: SeatNode = this.map.getSeatNodeById(sceneSeatId);
          this.discardSeatReservation(seatNode, false);
        });
      });


    this.canceledTicketReservations = this.messageBus
      .of(TicketReservationCancelRequest)
      .subscribe(message => {
        this.discardSeat(message.eventId, message.sceneSeatId);
      });
  }

  ngOnDestroy() {
    this.reservedSeatsSubscription.unsubscribe();
    this.userReservationsSubscription.unsubscribe();
    this.removeReservationRequestSubscription.unsubscribe();
    this.canceledTicketReservations.unsubscribe();
  }

  seatSelected(seat: SeatNode) {
    const state = seat.getSeatNodeState();

    if (state === SeatNodeState.Free) {
      this.reserveSeat(seat);
    } else {
      this.discardSeatReservation(seat);
    }
  }

  reserveSeat(seat: SeatNode) {
    this.withOrderContext()
      .switchMap(context => this.clientReservationsService.createClientReservation(
        context.eventId,
        context.client.clientId,
        context.order.id,
        seat.getSceneSeatId()))
      .subscribe(response => {
        seat.markAsSelected();
        this.userSelectedSeats.push(seat.getSceneSeatId());

        this.withOrderContext()
          .subscribe(context => {
            const eventId = context.eventId;
            const clientId = context.client.clientId;
            const orderId = context.order.id;
            const sceneSeatId = seat.getSceneSeatId();
            const scenePriceZoneId = seat.getPriceZoneId();

            const message = new SeatReservedMessage(eventId, sceneSeatId, scenePriceZoneId, clientId, orderId);
            this.messageBus.publish(message);
          });
      });
  }

  discardSeatReservation(seat: SeatNode, emit: boolean = true) {
    this.withOrderContext()
      .subscribe(response => {
        seat.markAsFree();
        const index = this.userSelectedSeats.indexOf(seat.getSceneSeatId(), 0);
        if (index > -1) {
          this.userSelectedSeats.splice(index, 1);
        }

        this.withOrderContext()
          .subscribe(context => {
            if (emit) {
              this.messageBus.publish(new SeatDiscardRequest(context.eventId, seat.getSceneSeatId()));
            }
          });
      });
  }

  discardSeat(eventId: number, sceneSeatId: number): void {
    this.withOrderContext()
      .switchMap(context => this.clientReservationsService.discardReservation(
        context.eventId,
        context.client.clientId,
        context.order.id,
        sceneSeatId))
      .subscribe(x => {
        const seatNode: SeatNode = this.map.getSeatNodeById(sceneSeatId);
        seatNode.markAsFree()
      })
  }

  withOrderContext() {
    return this.eventIdSubject
      .asObservable()
      .zip(this.activeOrderSubject.asObservable(), this.currentClientSubject.asObservable())
      .map(zip => {
        return { eventId: zip[0], order: zip[1], client: zip[2] }
      });
  }
}
