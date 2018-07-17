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
import { BaseComponent } from 'app/core/BaseComponent';

@Component({
  selector: 'app-basket-main-view',
  templateUrl: './basket-main-view.component.html',
  styleUrls: ['./basket-main-view.component.css']
})
export class BasketMainViewComponent extends BaseComponent implements OnInit, OnDestroy {
  activeOrder: ClientOrderDto;
  client: UserClientDto;
  user: Oidc.User;

  clientSubscription: Subscription;

  constructor(private route: ActivatedRoute,
    messageBus: MessageBus,
    private userClientService: UserClientService,
    private securityService: SecurityService,
    private ordersService: ClientsOrdersService,
    private ticketOptionsService: TicketOptionsService) {
      super(messageBus)
  }

  ngOnInit() {
    this.clientSubscription = this.subscribeClientObservable();
  }

  ngOnDestroy(): void {
    this.clientSubscription.unsubscribe();
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
