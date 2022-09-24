/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { TaxadeirService } from './taxadeir.service';

describe('Service: Taxadeirservice', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [TaxadeirService]
    });
  });

  it('should ...', inject([TaxadeirService], (service: TaxadeirService) => {
    expect(service).toBeTruthy();
  }));
});
