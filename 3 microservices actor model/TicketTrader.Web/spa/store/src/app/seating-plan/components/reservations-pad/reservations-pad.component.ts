import { Component, OnInit, Input, OnDestroy, SimpleChange, ViewChild } from '@angular/core';
import { UserClientDto, ClientOrderDto, PriceZoneListItemDto, TicketOrderDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';
import { User } from 'oidc-client';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { MessageBus } from 'app/messages/message-bus';
import {
  SeatReservedMessage,
  SeatDiscardedMessage,
  TicketReserveRequest,
  SeatDiscardRequest,
  TicketReservationCancelRequest
} from 'app/seating-plan/messages/messages';
import { Subscription } from 'rxjs/Subscription';
import { TicketOptionsDialogComponent } from 'app/seating-plan/components/ticket-options-dialog/ticket-options-dialog.component';
import { BaseComponent } from 'app/core/BaseComponent';
import { OrderTicketsService } from 'app/services/order-tickets.service';
import { ClientReservationsService } from 'app/services/client-reservations.service';

@Component({
  selector: 'app-reservations-pad',
  templateUrl: './reservations-pad.component.html',
  styleUrls: ['./reservations-pad.component.css']
})
export class ReservationsPadComponent extends BaseComponent implements OnInit, OnDestroy {
  eventOrdersSubscription: Subscription;
  tickets: TicketOrderDto[];

  reserveTicketRequestsSubscription: Subscription;
  discardeddSeatsSubscription: Subscription;
  eventIdSubject: ReplaySubject<number>
  activeOrderSubject: ReplaySubject<ClientOrderDto>;
  currentClientSubject: ReplaySubject<UserClientDto>;
  currentUserSubject: ReplaySubject<User>;

  @Input() set eventId(eventId: number) { this.notifyPropertyChanged(this.eventIdSubject, eventId); }
  @Input() set activeOrder(activeOrder: ClientOrderDto) { this.notifyPropertyChanged(this.activeOrderSubject, activeOrder); }
  @Input() set currentClient(currentClient: UserClientDto) { this.notifyPropertyChanged(this.currentClientSubject, currentClient); }
  @Input() set currentUser(currentUser: User) { this.notifyPropertyChanged(this.currentUserSubject, currentUser); }

  constructor(messageBus: MessageBus,
    private orderTicketsService: OrderTicketsService,
    private clientReservationsService: ClientReservationsService,
  ) {
    super(messageBus);
    this.eventIdSubject = new ReplaySubject<number>();
    this.activeOrderSubject = new ReplaySubject<ClientOrderDto>();
    this.currentClientSubject = new ReplaySubject<UserClientDto>();
    this.currentUserSubject = new ReplaySubject<User>();

    this.tickets = [];
  }

  ngOnInit() {
    this.discardeddSeatsSubscription = this.messageBus
      .of(SeatDiscardRequest)
      .subscribe(message => {
        const ticket = this.tickets.find(t => t.sceneSeatIds.findIndex(s => s == message.sceneSeatId.toString()) !== -1);
        if (ticket != null) {
          this.removeTicket(ticket, false)
        }
      });

    this.reserveTicketRequestsSubscription = this.messageBus
      .of(TicketReserveRequest)
      .switchMap(message => {
        return this.orderTicketsService.orderTicket(message.eventId.toString(),
          message.clientId,
          message.orderId,
          message.sceneSeatId.toString(),
          message.selectedOption,
          message.priceZoneName,
          message.optionName,
          message.grossAmount)
      })
      .subscribe(ticket => {
        this.tickets.push(ticket)
      })

    this.eventOrdersSubscription = this.withOrderContext()
      .switchMap(context => this.orderTicketsService.getClientTicketsForEvent(context.eventId.toString(), context.client.clientId, context.order.id))
      .subscribe(tickets => {
        this.tickets = tickets;
      });
  }

  ngOnDestroy(): void {
    this.discardeddSeatsSubscription.unsubscribe();
    this.reserveTicketRequestsSubscription.unsubscribe();
    this.eventOrdersSubscription.unsubscribe();
  }

  removeTicket(ticket: TicketOrderDto, emit: boolean = true): void {
    this.withOrderContext()
      .switchMap(context => this.orderTicketsService
        .removeTicket(context.client.clientId, context.order.id, ticket.eventId, ticket.id).map(x => context))
      .subscribe(context => {
        if (emit === true) {
          this.messageBus.publish(new SeatDiscardedMessage(context.eventId, ticket.sceneSeatIds))
        }

        const index = this.tickets.findIndex(x => x.id === ticket.id)
        if (index > -1) {
          this.tickets.splice(index, 1);
        }
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
