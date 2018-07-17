import { TestBed, inject } from '@angular/core/testing';

import { ClientsOrdersService } from './clients-orders.service';

describe('ClientsOrdersService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClientsOrdersService]
    });
  });

  it('should be created', inject([ClientsOrdersService], (service: ClientsOrdersService) => {
    expect(service).toBeTruthy();
  }));
});
