import { TestBed, inject } from '@angular/core/testing';

import { TicketOptionsService } from './ticket-options.service';

describe('TicketOptionsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TicketOptionsService]
    });
  });

  it('should be created', inject([TicketOptionsService], (service: TicketOptionsService) => {
    expect(service).toBeTruthy();
  }));
});
