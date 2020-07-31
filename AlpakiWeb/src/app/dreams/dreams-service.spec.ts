import { TestBed } from '@angular/core/testing';

import { DreamsService } from './dreams-service';

describe('DreamsServiceService', () => {
  let service: DreamsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DreamsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
