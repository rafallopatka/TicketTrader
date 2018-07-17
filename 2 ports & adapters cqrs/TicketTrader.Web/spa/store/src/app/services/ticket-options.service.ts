import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { Observable } from 'rxjs/Observable';
import { PriceZoneListItemDto } from 'app/services/api/app.clients';

@Injectable()
export class TicketOptionsService {

  constructor(private apiClientsFactory: ApiClientsFactory) { }

  getEventTicketsOptions(eventId: number): Observable<PriceZoneListItemDto[]> {
    const client = this.apiClientsFactory.createEventsPriceZonesService();
    return client
      .get(eventId)
      .map(s => s.result);
  }
}
