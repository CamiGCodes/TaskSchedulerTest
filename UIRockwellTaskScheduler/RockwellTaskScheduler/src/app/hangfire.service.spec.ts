import { TestBed } from '@angular/core/testing';

import { HangfireService } from './hangfire.service';

describe('HangfireService', () => {
  let service: HangfireService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HangfireService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
