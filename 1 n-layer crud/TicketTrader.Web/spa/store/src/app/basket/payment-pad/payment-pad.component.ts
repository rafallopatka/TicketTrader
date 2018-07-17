import { Component, OnInit, Input, OnDestroy, SimpleChange, ViewChild } from '@angular/core';
import { UserClientDto, ClientOrderDto, PriceZoneListItemDto, TicketOrderDto, PaymentTypeDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';
import { User } from 'oidc-client';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { MessageBus } from 'app/messages/message-bus';
import { SeatReservedMessage, SeatDiscardedMessage, TicketReserveRequest } from 'app/seating-plan/messages/messages';
import { Subscription } from 'rxjs/Subscription';
import { TicketOptionsDialogComponent } from 'app/seating-plan/components/ticket-options-dialog/ticket-options-dialog.component';
import { BaseComponent } from 'app/core/BaseComponent';
import { OrderTicketsService } from 'app/services/order-tickets.service';
import { ClientsOrdersService } from 'app/services/clients-orders.service';
import { Router } from '@angular/router';
import { PaymentsService } from 'app/services/payments.service';
import { OrderPaymentsService } from 'app/services/order-payments.service';

@Component({
  selector: 'app-payment-pad',
  templateUrl: './payment-pad.component.html',
  styleUrls: ['./payment-pad.component.css']
})
export class PaymentPadComponent extends BaseComponent implements OnInit, OnDestroy {
  payments: PaymentTypeDto[];
  selectedOption: number;

  activeOrderSubject: ReplaySubject<ClientOrderDto>;
  currentClientSubject: ReplaySubject<UserClientDto>;
  currentUserSubject: ReplaySubject<User>;

  @Input() set activeOrder(activeOrder: ClientOrderDto) { this.notifyPropertyChanged(this.activeOrderSubject, activeOrder); }
  @Input() set currentClient(currentClient: UserClientDto) { this.notifyPropertyChanged(this.currentClientSubject, currentClient); }
  @Input() set currentUser(currentUser: User) { this.notifyPropertyChanged(this.currentUserSubject, currentUser); }

  constructor(messageBus: MessageBus,
    private orderTicketsService: OrderTicketsService,
    private ordersService: ClientsOrdersService,
    private paymentService: PaymentsService,
    private orderPaymentService: OrderPaymentsService,
    private router: Router) {
    super(messageBus);
    this.activeOrderSubject = new ReplaySubject<ClientOrderDto>();
    this.currentClientSubject = new ReplaySubject<UserClientDto>();
    this.currentUserSubject = new ReplaySubject<User>();
  }

  ngOnInit() {
    this.withOrderContext()
      .switchMap(context => this.paymentService.getPaymentTypes().map(result => {
        return {
          context: context,
          payments: result
        }
      }))
      .switchMap(contextPayments => {
        return this.orderPaymentService
          .getSelectedPaymentOption(contextPayments.context.client.clientId, contextPayments.context.order.id)
          .map(selectedOption => {
            return {
              selectedOption: selectedOption,
              payments: contextPayments.payments
            }
          })
      })
      .subscribe(result => {
        let optionId = null;
        if (result.selectedOption == null) {
          optionId = result.payments[0].id
          this.updateOrderPaymentOption(optionId)
        } else {
          optionId = result.selectedOption.paymentId
        }

        this.payments = result.payments
        this.selectedOption = optionId;
      })
  }

  ngOnDestroy(): void {
  }

  finalizeOrder() {
    this.withOrderContext()
      .switchMap(ctx => this.ordersService.commitOrder(ctx.client.clientId, ctx.order.id))
      .subscribe(response => {
        this.router.navigate(['Summary'])
      })
  }

  withOrderContext() {
    return this.activeOrderSubject.asObservable()
      .zip(this.currentClientSubject.asObservable())
      .map(zip => {
        return { order: zip[0], client: zip[1] }
      });
  }

  onPaymentOptionChanged(option: PaymentTypeDto) {
    this.selectedOption = option.id;
    this.updateOrderPaymentOption(option.id)
  }

  updateOrderPaymentOption(optionId: number) {
    this.withOrderContext()
      .switchMap(x => this.orderPaymentService.selectPaymentOption(x.client.clientId, x.order.id, optionId))
      .subscribe()
  }
}
