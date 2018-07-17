import { Injectable } from '@angular/core';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { Observable } from 'rxjs/Observable';
import { PaymentTypeDto } from 'app/services/api/app.clients';

@Injectable()
export class PaymentsService {

  constructor(
    private apiFactory: ApiClientsFactory
  ) { }


  getPaymentTypes(): Observable<PaymentTypeDto[]> {
    const client = this.apiFactory.createPaymentsService()
    return client
      .get()
      .map(x => x.result)
  }
}
