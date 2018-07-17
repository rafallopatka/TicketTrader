import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';

export class SeatReservationDto {
  constructor(public sceneSeatId: number) { }
}

@Injectable()
export class ReservationService {
  constructor(private clientsFactory: ApiClientsFactory) { }

  getAllReservedSeats(eventId: number): Observable<SeatReservationDto[]> {
    const client = this.clientsFactory.createSeatsReservationsService();

    return client.get(eventId)
      .map(x => x.result.map(s => new SeatReservationDto(s.sceneSeatId)));
  }
}
