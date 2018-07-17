import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { TicketOrderDto, SeatPriceOptionDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class OrderTicketsService {

  constructor(private apiFactory: ApiClientsFactory) {
  }

  orderTicket(eventId: number, clientId: number, orderId: number, sceneSeatId: number, selectedOption: number): Observable<TicketOrderDto> {
    const service = this.apiFactory.createClientsOrdersTicketsService();

    const priceOption = new SeatPriceOptionDto({
      priceOptionId: selectedOption,
      sceneSeatId: sceneSeatId
    });

    return service
      .post(eventId, clientId, orderId, priceOption)
      .map(response => response.result);
  }

  getClientTicketsForEvent(eventId: number, clientId: number, orderId): Observable<TicketOrderDto[]> {
    const service = this.apiFactory.createClientsOrdersTicketsService();
    return service
      .get2(eventId, clientId, orderId)
      .map(response => response.result)
  }

  getClientTickets(clientId: number, orderId: number): Observable<TicketOrderDto[]> {
    const service = this.apiFactory.createClientsOrdersTicketsService();

    return service
      .get(clientId, orderId)
      .map(response => response.result)
  }

  removeTicket(clientId: number, orderId: number, eventId: number, ticketId: number): Observable<boolean> {
    const service = this.apiFactory.createClientsOrdersTicketsService();
    return service
      .delete(eventId, clientId, orderId, ticketId)
      .map(response => response.failure === false)
  }
}
