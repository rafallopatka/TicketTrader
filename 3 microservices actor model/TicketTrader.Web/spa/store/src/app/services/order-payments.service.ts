import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { OrderPaymentDto } from 'app/services/api/app.clients';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class OrderPaymentsService {

  constructor(private apiFactory: ApiClientsFactory) { }

  getSelectedPaymentOption(clientId: string, orderId: string): Observable<OrderPaymentDto> {
    const service = this.apiFactory.createClientsOrdersPaymentsService();

    return service
      .get(clientId, orderId)
      .map(x => {
        return x.result.length > 0 ? x.result[0] : null
      })
  }

  selectPaymentOption(clientId: string, orderId: string, optionId: string): Observable<boolean> {
    const client = this.apiFactory.createClientsOrdersPaymentsService()
    return client
      .post(clientId, orderId, optionId)
      .map(x => x.failure === false)
  }
}
