import { TestBed, inject } from '@angular/core/testing';

import { OrderPaymentsService } from './order-payments.service';

describe('OrderPaymentsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OrderPaymentsService]
    });
  });

  it('should be created', inject([OrderPaymentsService], (service: OrderPaymentsService) => {
    expect(service).toBeTruthy();
  }));
});
