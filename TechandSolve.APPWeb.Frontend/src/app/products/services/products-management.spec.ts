import { TestBed } from '@angular/core/testing';

import { ProductsManagement } from './products-management';

describe('ProductsManagement', () => {
  let service: ProductsManagement;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ProductsManagement);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
