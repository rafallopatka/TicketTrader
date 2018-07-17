import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { OrderPaymentDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class OrderPaymentsService {

  constructor(private apiFactory: ApiClientsFactory) { }

  getSelectedPaymentOption(clientId: number, orderId: number): Observable<OrderPaymentDto> {
    const service = this.apiFactory.createClientsOrdersPaymentsService();

    return service
      .get(clientId, orderId)
      .map(x => {
        return x.result.length > 0 ? x.result[0] : null
      })
  }

  selectPaymentOption(clientId: number, orderId: number, optionId: number): Observable<boolean> {
    const client = this.apiFactory.createClientsOrdersPaymentsService()
    return client
      .post(clientId, orderId, optionId)
      .map(x => x.failure === false)
  }
}
