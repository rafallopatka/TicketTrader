import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { TicketOrderDto, SeatPriceOptionDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';


@Injectable()
export class OrderTicketsService {

  constructor(private apiFactory: ApiClientsFactory) {
  }

  orderTicket(eventId: string, 
    clientId: string, 
    orderId: string, 
    sceneSeatId: string, 
    selectedOption: string,
    priceZoneName: string,
    priceOptionName: string,
    grossAmount: number): Observable<TicketOrderDto> {

    const service = this.apiFactory.createClientsOrdersTicketsService();

    const priceOption = new SeatPriceOptionDto({
      priceOptionId: selectedOption,
      sceneSeatId: sceneSeatId,
      priceZoneName: priceZoneName,
      priceOptionName: priceOptionName,
      grossAmount: grossAmount
    });

    return service
      .post(eventId, clientId, orderId, priceOption)
      .map(response => response.result);
  }

  getClientTicketsForEvent(eventId: string, clientId: string, orderId: string): Observable<TicketOrderDto[]> {
    const service = this.apiFactory.createClientsOrdersTicketsService();
    return service
      .get2(eventId, clientId, orderId)
      .map(response => response.result)
  }

  getClientTickets(clientId: string, orderId: string): Observable<TicketOrderDto[]> {
    const service = this.apiFactory.createClientsOrdersTicketsService();

    return service
      .get(clientId, orderId)
      .map(response => response.result)
  }

  removeTicket(clientId: string, orderId: string, eventId: string, ticketId: string): Observable<boolean> {
    const service = this.apiFactory.createClientsOrdersTicketsService();
    return service
      .delete(eventId, clientId, orderId, ticketId)
      .map(response => response.failure === false)
  }
}
