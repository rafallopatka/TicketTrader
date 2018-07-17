import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AppComponent } from './app.component';
import { AppRoutingModule } from './app-routing.module';
import { PageNotFoundComponent } from './access-control/page-not-found.component';

import { SeatingPlanModule } from './seating-plan/seating-plan.module'
import { BasketModule } from './basket/basket.module'
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';

import { ForbiddenComponent } from './access-control/forbidden.component';
import { AuthorizeComponent } from './access-control/authorize.component';
import { SecurityService } from 'app/services/security.service';

import { TempDataService } from 'app/services/TempDataService';
import { AuthHttp } from 'app/services/api/auth-http';
import { AuthGuard } from 'app/services/auth.guard';
import { BannedGuard } from 'app/services/banned.guard';
import { EventGuard } from 'app/services/event.guard';
import { PostAuthorizeRedirectComponent } from 'app/access-control/post-authorize-redirect.component';
import { UserClientService } from 'app/services/user-client.service';
import { CacheService } from 'app/services/cache.service';
import { ClientsOrdersService } from 'app/services/clients-orders.service';
import { MessageBus } from 'app/messages/message-bus';
import { ClientReservationsService } from 'app/services/client-reservations.service';
import { ClientsOrdersReservationsClient } from 'app/services/api/app.clients';
import { TicketOptionsService } from 'app/services/ticket-options.service';
import { FormsModule } from '@angular/forms';
import { OrderTicketsService } from 'app/services/order-tickets.service';
import { EventDetailsService } from 'app/services/event-details.service';
import { PaymentsService } from 'app/services/payments.service';
import { OrderPaymentsService } from 'app/services/order-payments.service';
import { DeliveriesService } from 'app/services/deliveries.service';
import { OrderDeliveriesService } from 'app/services/order-deliveries.service';

@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent,
    ForbiddenComponent,
    AuthorizeComponent,
    PostAuthorizeRedirectComponent,
  ],
  imports: [
    FormsModule,
    BrowserModule,
    SeatingPlanModule,
    BasketModule,
    AppRoutingModule,
  ],
  providers: [ApiClientsFactory,
    SecurityService,
    AuthGuard,
    BannedGuard,
    EventGuard,
    TempDataService,
    CacheService,
    AuthHttp,
    UserClientService,
    ClientsOrdersService,
    ClientReservationsService,
    ClientsOrdersReservationsClient,
    TicketOptionsService,
    OrderTicketsService,
    EventDetailsService,
    PaymentsService,
    OrderPaymentsService,
    DeliveriesService,
    OrderDeliveriesService,
    MessageBus],
  bootstrap: [AppComponent]
})
export class AppModule {
  constructor() {

  }
}
