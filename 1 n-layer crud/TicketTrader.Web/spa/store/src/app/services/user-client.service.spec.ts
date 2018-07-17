import { TestBed, inject } from '@angular/core/testing';

import { UserClientService } from './user-client.service';

describe('UserClientService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [UserClientService]
    });
  });

  it('should be created', inject([UserClientService], (service: UserClientService) => {
    expect(service).toBeTruthy();
  }));
});
