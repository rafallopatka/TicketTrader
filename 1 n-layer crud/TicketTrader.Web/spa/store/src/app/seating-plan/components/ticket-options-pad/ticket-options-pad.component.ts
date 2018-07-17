import { Component, OnInit, OnDestroy, Input } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Subscription } from 'rxjs/Subscription';
import { Observable } from 'rxjs/Observable';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { PriceZoneListItemDto } from 'app/services/api/app.clients';
import { ReplaySubject } from 'rxjs/ReplaySubject';
import { BaseComponent } from 'app/core/BaseComponent';
import { MessageBus } from 'app/messages/message-bus';

@Component({
  selector: 'app-ticket-options-pad',
  templateUrl: './ticket-options-pad.component.html',
  styleUrls: ['./ticket-options-pad.component.css']
})
export class TicketOptionsPadComponent extends BaseComponent implements OnInit, OnDestroy {
  priceZonesObservable: Observable<PriceZoneListItemDto[]>;

  eventIdSubject: ReplaySubject<number>
  ticketOptionsSubject: ReplaySubject<PriceZoneListItemDto[]>;

  @Input() set eventId(eventId: number) { this.notifyPropertyChanged(this.eventIdSubject, eventId); }
  @Input() set ticketOptions(options: PriceZoneListItemDto[]) { this.notifyPropertyChanged(this.ticketOptionsSubject, options); }

  constructor(messageBus: MessageBus) {
    super(messageBus);
    this.eventIdSubject = new ReplaySubject<number>();
    this.ticketOptionsSubject = new ReplaySubject<PriceZoneListItemDto[]>();
  }

  ngOnInit() {
    this.priceZonesObservable = this.ticketOptionsSubject.asObservable();
  }

  ngOnDestroy(): void {
  }
}
