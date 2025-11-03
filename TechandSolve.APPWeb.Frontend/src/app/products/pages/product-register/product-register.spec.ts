import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductRegister } from './product-register';

describe('ProductRegister', () => {
  let component: ProductRegister;
  let fixture: ComponentFixture<ProductRegister>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductRegister]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductRegister);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
