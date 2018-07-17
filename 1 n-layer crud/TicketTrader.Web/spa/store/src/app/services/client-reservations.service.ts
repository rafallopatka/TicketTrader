import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { SeatReservationDto } from 'app/services/reservation.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ClientReservationsService {

  constructor(private apiFactory: ApiClientsFactory) { }

  getClientReservations(eventId: number, clientId: number, orderId: number): Observable<SeatReservationDto[]> {
    const service = this.apiFactory.createClientsOrdersReservationsService();

    return service
      .get(eventId, clientId, orderId)
      .map(response => response.result);
  }

  createClientReservation(eventId: number, clientId: number, orderId: number, sceneNodeId: number): Observable<SeatReservationDto> {
    const service = this.apiFactory.createClientsOrdersReservationsService();
    return service
      .post(eventId, clientId, orderId, sceneNodeId)
      .map(x => x.result);
  }

  discardReservation(eventId: number, clientId: number, orderId: number, sceneNodeId: number): Observable<boolean> {
    const service = this.apiFactory.createClientsOrdersReservationsService();
    return service.delete(eventId, clientId, orderId, sceneNodeId)
      .map(x => x.failure === false);
  }
}
