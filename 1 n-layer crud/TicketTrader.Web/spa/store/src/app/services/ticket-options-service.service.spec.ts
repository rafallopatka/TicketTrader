import { TestBed, inject } from '@angular/core/testing';

import { TicketOptionsServiceService } from './ticket-options-service.service';

describe('TicketOptionsServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TicketOptionsServiceService]
    });
  });

  it('should be created', inject([TicketOptionsServiceService], (service: TicketOptionsServiceService) => {
    expect(service).toBeTruthy();
  }));
});
