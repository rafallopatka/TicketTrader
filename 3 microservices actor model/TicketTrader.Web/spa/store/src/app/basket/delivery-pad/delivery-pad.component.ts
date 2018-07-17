import { Component, OnInit, Input, OnDestroy, SimpleChange, ViewChild } from '@angular/core';
import { UserClientDto, ClientOrderDto, PriceZoneListItemDto, TicketOrderDto, DeliveryTypeDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';
import { User } from 'oidc-client';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { MessageBus } from 'app/messages/message-bus';
import { SeatReservedMessage, SeatDiscardedMessage, TicketReserveRequest } from 'app/seating-plan/messages/messages';
import { Subscription } from 'rxjs/Subscription';
import { TicketOptionsDialogComponent } from 'app/seating-plan/components/ticket-options-dialog/ticket-options-dialog.component';
import { BaseComponent } from 'app/core/BaseComponent';
import { OrderTicketsService } from 'app/services/order-tickets.service';
import { DeliveriesService } from 'app/services/deliveries.service';
import { OrderDeliveriesService } from 'app/services/order-deliveries.service';

@Component({
  selector: 'app-delivery-pad',
  templateUrl: './delivery-pad.component.html',
  styleUrls: ['./delivery-pad.component.css']
})
export class DeliveryPadComponent extends BaseComponent implements OnInit, OnDestroy {
  deliveries: DeliveryTypeDto[];
  selectedOption: string;

  activeOrderSubject: ReplaySubject<ClientOrderDto>;
  currentClientSubject: ReplaySubject<UserClientDto>;
  currentUserSubject: ReplaySubject<User>;

  @Input() set activeOrder(activeOrder: ClientOrderDto) { this.notifyPropertyChanged(this.activeOrderSubject, activeOrder); }
  @Input() set currentClient(currentClient: UserClientDto) { this.notifyPropertyChanged(this.currentClientSubject, currentClient); }
  @Input() set currentUser(currentUser: User) { this.notifyPropertyChanged(this.currentUserSubject, currentUser); }

  constructor(messageBus: MessageBus,
    private orderTicketsService: OrderTicketsService,
    private deliveryService: DeliveriesService,
    private orderDeliveriesService: OrderDeliveriesService) {
    super(messageBus);
    this.activeOrderSubject = new ReplaySubject<ClientOrderDto>();
    this.currentClientSubject = new ReplaySubject<UserClientDto>();
    this.currentUserSubject = new ReplaySubject<User>();
  }

  ngOnInit() {
    this.withOrderContext()
    .switchMap(context => this.deliveryService.getDeliveryTypes().map(result => {
      return {
        context: context,
        deliveries: result
      }
    }))
    .switchMap(contextDeliveries => {
      return this.orderDeliveriesService
        .getSelectedDeliveryOption(contextDeliveries.context.client.clientId, contextDeliveries.context.order.id)
        .map(selectedOption => {
          return {
            selectedOption: selectedOption,
            deliveries: contextDeliveries.deliveries
          }
        })
    })
    .subscribe(result => {
      let optionId = null;
      if (result.selectedOption == null) {
        optionId = result.deliveries[0].id
        this.updateOrderDeliveryOption(optionId)
      } else {
        optionId = result.selectedOption.deliveryId
      }

      this.deliveries = result.deliveries
      this.selectedOption = optionId;
    })
  }

  ngOnDestroy(): void {
  }

  withOrderContext() {
    return this.activeOrderSubject.asObservable()
      .zip(this.currentClientSubject.asObservable())
      .map(zip => {
        return { order: zip[0], client: zip[1] }
      });
  }

  onDeliveryOptionChanged(option: DeliveryTypeDto) {
    this.selectedOption = option.id;
    this.updateOrderDeliveryOption(option.id)
  }

  updateOrderDeliveryOption(optionId: string) {
    this.withOrderContext()
      .switchMap(x => this.orderDeliveriesService.selectDeliveryOption(x.client.clientId, x.order.id, optionId))
      .subscribe()
  }
}
