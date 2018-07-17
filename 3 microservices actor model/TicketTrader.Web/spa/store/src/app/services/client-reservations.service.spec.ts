import { TestBed, inject } from '@angular/core/testing';

import { ClientReservationsService } from './client-reservations.service';

describe('ClientReservationsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [ClientReservationsService]
    });
  });

  it('should be created', inject([ClientReservationsService], (service: ClientReservationsService) => {
    expect(service).toBeTruthy();
  }));
});
