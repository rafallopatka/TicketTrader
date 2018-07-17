import { TestBed, inject } from '@angular/core/testing';

import { OrderTicketsService } from './order-tickets.service';

describe('OrderTicketsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [OrderTicketsService]
    });
  });

  it('should be created', inject([OrderTicketsService], (service: OrderTicketsService) => {
    expect(service).toBeTruthy();
  }));
});
