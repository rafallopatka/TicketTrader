import { Injectable } from '@angular/core';
import { DeliveryTypeDto } from 'app/services/api/app.clients';
import { ApiClientsFactory } from 'app/services/api/app.api-client.factory';
import { Observable } from 'rxjs/Observable';

@Injectable()
export class DeliveriesService {

  constructor(
    private apiFactory: ApiClientsFactory
  ) { }


  getDeliveryTypes(): Observable<DeliveryTypeDto[]> {
    const client = this.apiFactory.createDeliveriesService()
    return client
      .get()
      .map(x => x.result)
  }
}
