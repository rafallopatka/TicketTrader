import { Component, OnInit, OnDestroy, OnChanges, SimpleChange, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { MessageBus } from 'app/messages/message-bus';
import { EventViewVisitedMessage } from 'app/seating-plan/messages/messages';
import { Observable } from 'rxjs/Observable';
import { UserClientService } from 'app/services/user-client.service';
import { SecurityService } from 'app/services/security.service';
import { UserClientLoadedMessage } from 'app/messages/messages';
import { ClientsOrdersService } from 'app/services/clients-orders.service';
import { UserClientDto, ClientOrderDto, PriceZoneListItemDto } from 'app/services/api/app.clients';
import { User } from 'oidc-client';
import { ClientReservationsService } from 'app/services/client-reservations.service';
import { TicketOptionsService } from 'app/services/ticket-options.service';

@Component({
  selector: 'app-seating-plan-main-view',
  templateUrl: './seating-plan-main-view.component.html',
  styleUrls: ['./seating-plan-main-view.component.css']
})
export class SeatingPlanMainViewComponent implements OnInit, OnDestroy, OnChanges {
  eventIdSubscription: Subscription;
  ticketOptionsSubscription: Subscription;
  clientSubscription: Subscription;

  ticketOptions: PriceZoneListItemDto[];
  eventId: number;
  activeOrder: ClientOrderDto;
  client: UserClientDto;
  user: Oidc.User;

  constructor(private route: ActivatedRoute,
    private messageBus: MessageBus,
    private userClientService: UserClientService,
    private securityService: SecurityService,
    private ordersService: ClientsOrdersService,
    private ticketOptionsService: TicketOptionsService) {
  }

  ngOnInit() {
    const eventIdObservable = this.route
      .params
      .map(params => +params['eventId']);

    this.eventIdSubscription = this.subscribeEventIdObservable(eventIdObservable);
    this.ticketOptionsSubscription = this.subscrbeTicketOptionsObservable(eventIdObservable);
    this.clientSubscription = this.subscribeClientObservable();
  }

  ngOnDestroy(): void {
    this.clientSubscription.unsubscribe();
    this.eventIdSubscription.unsubscribe();
    this.ticketOptionsSubscription.unsubscribe();
  }

  ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
    console.log(changes);
  }

  subscribeEventIdObservable(eventIdObservable: Observable<number>): Subscription {
    return eventIdObservable
      .subscribe(eventId => {
        this.eventId = eventId;
        this.messageBus.publish(new EventViewVisitedMessage(eventId));
      });

  }

  subscrbeTicketOptionsObservable(eventIdObservable: Observable<number>): Subscription {
    return eventIdObservable
      .switchMap(eventId => this.ticketOptionsService.getEventTicketsOptions(eventId))
      .subscribe(options => {
        this.ticketOptions = options;
      });
  }

  subscribeClientObservable(): Subscription {
    return this.securityService
      .getUser()
      .switchMap(user => this.userClientService
        .getUserClient(user)
        .map(client => {
          return {
            user,
            client
          };
        })
      )
      .subscribe(res => {
        this.user = res.user;
        this.client = res.client;

        this.ordersService
          .getOrCreateActiveOrder(this.client.clientId)
          .subscribe(order => {
            this.activeOrder = order;
          })
      });
  }
}
