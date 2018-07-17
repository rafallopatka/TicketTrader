import { TestBed, inject } from '@angular/core/testing';

import { ClientReservationsServiceService } from './client-reservations-service.service';

describe('ClientReservationsServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClientReservationsServiceService]
    });
  });

  it('should be created', inject([ClientReservationsServiceService], (service: ClientReservationsServiceService) => {
    expect(service).toBeTruthy();
  }));
});
