import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { SeatReservationDto } from 'app/services/reservation.service';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ClientReservationsService {

  constructor(private apiFactory: ApiClientsFactory) { }

  getClientReservations(eventId: string, clientId: string, orderId: string): Observable<SeatReservationDto[]> {
    const service = this.apiFactory.createClientsOrdersReservationsService();

    return service
      .get(eventId, clientId, orderId)
      .map(response => response.result.map(reservation => {
        
        return new SeatReservationDto(reservation.id, reservation.sceneSeatId)
      }));
  }

  createClientReservation(eventId: string, clientId: string, orderId: string, sceneNodeId: string): Observable<SeatReservationDto> {
    const service = this.apiFactory.createClientsOrdersReservationsService();
    return service
      .post(eventId, clientId, orderId, sceneNodeId)
      .map(response => {
        
        let reservation = response.result
        return new SeatReservationDto(reservation.id, reservation.sceneSeatId)
      });
  }

  discardReservation(eventId: string, clientId: string, orderId: string, sceneNodeId: string, reservationId: string): Observable<boolean> {
    const service = this.apiFactory.createClientsOrdersReservationsService();
    return service.delete(eventId, clientId, orderId, sceneNodeId, reservationId)
      .map(x => x.failure === false);
  }
}
