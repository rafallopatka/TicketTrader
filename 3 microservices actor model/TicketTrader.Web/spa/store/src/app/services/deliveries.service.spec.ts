import { TestBed, inject } from '@angular/core/testing';

import { DeliveriesService } from './deliveries.service';

describe('DeliveriesService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DeliveriesService]
    });
  });

  it('should be created', inject([DeliveriesService], (service: DeliveriesService) => {
    expect(service).toBeTruthy();
  }));
});
