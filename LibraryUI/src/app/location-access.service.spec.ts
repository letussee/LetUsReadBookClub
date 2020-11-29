import { TestBed } from '@angular/core/testing';

import { LocationAccessService } from './location-access.service';

describe('LocationAccessService', () => {
  let service: LocationAccessService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(LocationAccessService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
