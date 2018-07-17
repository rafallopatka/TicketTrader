import { TestBed, inject } from '@angular/core/testing';

import { UserClientServiceService } from './user-client-service.service';

describe('UserClientServiceService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserClientServiceService]
    });
  });

  it('should be created', inject([UserClientServiceService], (service: UserClientServiceService) => {
    expect(service).toBeTruthy();
  }));
});
