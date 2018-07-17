import { TestBed, inject } from '@angular/core/testing';

import { OrderDeliveriesService } from './order-deliveries.service';

describe('OrderDeliveriesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OrderDeliveriesService]
    });
  });

  it('should be created', inject([OrderDeliveriesService], (service: OrderDeliveriesService) => {
    expect(service).toBeTruthy();
  }));
});
