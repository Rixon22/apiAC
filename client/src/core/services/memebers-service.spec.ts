import { TestBed } from '@angular/core/testing';

import { MemebersService } from './memebers-service';

describe('MemebersService', () => {
  let service: MemebersService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MemebersService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
