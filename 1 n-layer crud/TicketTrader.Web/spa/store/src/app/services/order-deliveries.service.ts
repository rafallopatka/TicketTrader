import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { OrderDeliveryDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class OrderDeliveriesService {

  constructor(private apiFactory: ApiClientsFactory) { }

  getSelectedDeliveryOption(clientId: number, orderId: number): Observable<OrderDeliveryDto> {
    const service = this.apiFactory.createClientsOrdersDeliveriesService();

    return service
      .get(clientId, orderId)
      .map(x => {
        return x.result.length > 0 ? x.result[0] : null
      })
  }

  selectDeliveryOption(clientId: number, orderId: number, optionId: number): Observable<boolean> {
    const client = this.apiFactory.createClientsOrdersDeliveriesService()
    return client
      .post(clientId, orderId, optionId)
      .map(x => x.failure === false)
  }

}
