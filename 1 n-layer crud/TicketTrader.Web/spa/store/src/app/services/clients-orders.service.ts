import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { ClientOrderDto, ClientOrderState } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class ClientsOrdersService {

  constructor(private clientsFactory: ApiClientsFactory) { }

  getOrCreateActiveOrder(clientId: number): Observable<ClientOrderDto> {
    const service = this.clientsFactory.createClientsOrdersService();

    return service
      .get(clientId, ClientOrderState.Active)
      .map(x => x.result)
      .switchMap(order => {
        if (order == null || order.length === 0) {
          return this.createOrder(clientId);
        } else {
          return Observable.of(order[0])
        }
      });
  }

  createOrder(clientId: number): Observable<ClientOrderDto> {
    const service = this.clientsFactory.createClientsOrdersService();
    return service
      .post(clientId)
      .map(response => {
        return response.result;
      });
  }

  commitOrder(clientId: number, orderId: number): Observable<boolean> {
    const service = this.clientsFactory.createClientsOrdersService();
    return service
      .commit(clientId, orderId)
      .map(response => {
        return response.failure === false;
      });
  }
}
