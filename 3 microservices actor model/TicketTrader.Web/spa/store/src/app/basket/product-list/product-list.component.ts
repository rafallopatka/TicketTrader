import { Component, OnInit, Input, OnDestroy, SimpleChange, ViewChild } from '@angular/core';
import { UserClientDto, ClientOrderDto, PriceZoneListItemDto, TicketOrderDto, EventListItemDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';
import { User } from 'oidc-client';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { MessageBus } from 'app/messages/message-bus';
import { SeatReservedMessage, SeatDiscardedMessage, TicketReserveRequest } from 'app/seating-plan/messages/messages';
import { Subscription } from 'rxjs/Subscription';
import { TicketOptionsDialogComponent } from 'app/seating-plan/components/ticket-options-dialog/ticket-options-dialog.component';
import { BaseComponent } from 'app/core/BaseComponent';
import { OrderTicketsService } from 'app/services/order-tickets.service';
import { EventDetailsService } from 'app/services/event-details.service';

@Component({
  selector: 'app-product-list',
  templateUrl: './product-list.component.html',
  styleUrls: ['./product-list.component.css']
})
export class ProductListComponent extends BaseComponent implements OnInit, OnDestroy {
  tickets: TicketDetails[];
  ticketsSubscription: Subscription;

  activeOrderSubject: ReplaySubject<ClientOrderDto>;
  currentClientSubject: ReplaySubject<UserClientDto>;
  currentUserSubject: ReplaySubject<User>;

  @Input() set activeOrder(activeOrder: ClientOrderDto) { this.notifyPropertyChanged(this.activeOrderSubject, activeOrder); }
  @Input() set currentClient(currentClient: UserClientDto) { this.notifyPropertyChanged(this.currentClientSubject, currentClient); }
  @Input() set currentUser(currentUser: User) { this.notifyPropertyChanged(this.currentUserSubject, currentUser); }

  constructor(messageBus: MessageBus,
    private orderTicketsService: OrderTicketsService,
    private eventDetailsService: EventDetailsService) {
    super(messageBus);
    this.activeOrderSubject = new ReplaySubject<ClientOrderDto>();
    this.currentClientSubject = new ReplaySubject<UserClientDto>();
    this.currentUserSubject = new ReplaySubject<User>();

    this.tickets = [];
  }

  ngOnInit() {
    this.ticketsSubscription = this.withOrderContext()
      .switchMap(context => this.orderTicketsService.getClientTickets(context.client.clientId, context.order.id))
      .switchMap(tickets => {
        let events = tickets.map(t => Number(t.eventId))
        return this.eventDetailsService
          .getEventsDetails(events)
          .map(events => tickets
            .map(ticket => {
              return {
                ticket: ticket,
                event: events.filter(e => e.id.toString() === ticket.eventId)[0]
              };
            }));
      })
      .map(result => result.map(data => new TicketDetails(data.ticket, data.event)))
      .subscribe(result => {
        this.tickets = result;
      });
  }

  ngOnDestroy(): void {
    this.ticketsSubscription.unsubscribe();
  }

  removeTicket(ticket: TicketOrderDto): void {
    this.withOrderContext()
      .switchMap(context => this.orderTicketsService.removeTicket(context.client.clientId, context.order.id, ticket.eventId, ticket.id))
      .subscribe(r => {
        const index = this.tickets.findIndex(x => x.ticket.id === ticket.id)
        if (index > -1) {
          this.tickets.splice(index, 1);
        }
      })
  }

  withOrderContext() {
    return this.activeOrderSubject.asObservable()
      .zip(this.currentClientSubject.asObservable())
      .map(zip => {
        return { order: zip[0], client: zip[1] }
      });
  }
}

class TicketDetails {
  constructor(public ticket: TicketOrderDto, public event: EventListItemDto) {

  }
}
