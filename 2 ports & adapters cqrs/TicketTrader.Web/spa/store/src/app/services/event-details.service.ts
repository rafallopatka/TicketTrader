import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { Observable } from 'rxjs/Observable';
import { EventListItemDto } from 'app/services/api/app.clients';

@Injectable()
export class EventDetailsService {

  constructor(private apiFactory: ApiClientsFactory) {
  }

  getEventDetails(eventId: number): Observable<EventListItemDto> {
    const service = this.apiFactory.createEventsService();

    return service
      .get2(eventId)
      .map(response => response.result)
  }

  getEventsDetails(eventsIds: number[]): Observable<EventListItemDto[]> {
    const ids = new Set(eventsIds)

    const observables = Array.from(ids)
      .map(eventId => this.getEventDetails(eventId));

    return Observable.forkJoin(observables);
  }
}
