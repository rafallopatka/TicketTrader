import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { PriceZoneListItemDto, ClientOrderDto, UserClientDto, PriceOption } from 'app/services/api/app.clients';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { AppComponent } from 'app/app.component';
import { BaseComponent } from 'app/core/BaseComponent';
import { MessageBus } from 'app/messages/message-bus';
import { User } from 'oidc-client';
import {
  SeatReservedMessage,
  SeatDiscardRequest,
  TicketReserveRequest,
  TicketReservationCancelRequest
} from 'app/seating-plan/messages/messages';
import { Subscription } from 'rxjs/Subscription';

declare var $: any;

@Component({
  selector: 'app-ticket-options-dialog',
  templateUrl: './ticket-options-dialog.component.html',
  styleUrls: ['./ticket-options-dialog.component.css']
})
export class TicketOptionsDialogComponent extends BaseComponent implements OnInit, OnDestroy {
  context: { eventId: number; 
    clientId: string; 
    orderId: string; 
    sceneSeatId: number; 
    scenePriceZoneId: number; 
    selectedOption: number; 
    scenePriceZoneName: string, 
    selectedOptionName: string 
    grossAmount: number
  };

  selectedOption: number;
  seatPriceZone: PriceZoneListItemDto;
  reservedSeatsSubscription: Subscription;
  currentUserSubject: ReplaySubject<User>;
  currentClientSubject: ReplaySubject<UserClientDto>;
  activeOrderSubject: ReplaySubject<ClientOrderDto>;
  eventIdSubject: ReplaySubject<number>
  ticketOptionsSubject: ReplaySubject<PriceZoneListItemDto[]>;

  @Input() set eventId(eventId: number) { this.notifyPropertyChanged(this.eventIdSubject, eventId); }
  @Input() set activeOrder(activeOrder: ClientOrderDto) { this.notifyPropertyChanged(this.activeOrderSubject, activeOrder); }
  @Input() set currentClient(currentClient: UserClientDto) { this.notifyPropertyChanged(this.currentClientSubject, currentClient); }
  @Input() set currentUser(currentUser: User) { this.notifyPropertyChanged(this.currentUserSubject, currentUser); }
  @Input() set ticketOptions(options: PriceZoneListItemDto[]) { this.notifyPropertyChanged(this.ticketOptionsSubject, options); }

  constructor(messageBus: MessageBus) {
    super(messageBus);

    this.seatPriceZone = new PriceZoneListItemDto();
    this.ticketOptionsSubject = new ReplaySubject<PriceZoneListItemDto[]>();
    this.eventIdSubject = new ReplaySubject<number>();
    this.activeOrderSubject = new ReplaySubject<ClientOrderDto>();
    this.currentClientSubject = new ReplaySubject<UserClientDto>();
    this.currentUserSubject = new ReplaySubject<User>();
  }

  ngOnInit() {
    this.reservedSeatsSubscription = this.messageBus.of(SeatReservedMessage)
      .switchMap(message => this.ticketOptionsSubject.asObservable().map(priceZones => {
        return {
          message: message,
          priceZones: priceZones
        };
      }))
      .map(context => {
        const message = context.message;
        const priceZones = context.priceZones;
        return {
          eventId: message.eventId,
          sceneSeatId: message.sceneSeatId,
          scenePriceZoneId: message.scenePriceZoneId,
          clientId: message.clientId,
          orderId: message.orderId,
          priceZone: priceZones.filter(p => p.scenePriceZoneId === message.scenePriceZoneId)[0]
        };
      })
      .subscribe(x => {
        this.seatPriceZone = x.priceZone
        this.selectedOption = x.priceZone.options[0].id;

        const priceZone = this.selectedOption;
        this.context = {
          eventId: x.eventId,
          clientId: x.clientId,
          orderId: x.orderId,
          sceneSeatId: x.sceneSeatId,
          scenePriceZoneId: x.scenePriceZoneId,
          selectedOption: this.selectedOption,
          selectedOptionName: x.priceZone.options[0].name,
          scenePriceZoneName: x.priceZone.name,
          grossAmount: x.priceZone.options[0].grossAmount
        };

        $('#ticket-options-dialog').modal({
          backdrop: 'static',
          keyboard: false
        });
      });
  }

  ngOnDestroy(): void {
    this.reservedSeatsSubscription.unsubscribe();
  }

  onPriceOptionChanged(option: PriceOption) {
    this.context.selectedOption = option.id;
  }

  discard() {
    this.messageBus
      .publish(new TicketReservationCancelRequest(this.context.eventId, this.context.sceneSeatId.toString()));
  }

  reserve() {
    let selectedOption = this.selectedOption
    let priceOption = this.seatPriceZone.options.filter(s => s.id == selectedOption)[0]

    const message = new TicketReserveRequest(this.context.eventId,
      this.context.clientId,
      this.context.orderId,
      this.context.sceneSeatId,
      selectedOption.toString(),
      this.context.scenePriceZoneName,
      priceOption.name,
      priceOption.grossAmount);

    this.messageBus
      .publish(message);
  }
}
