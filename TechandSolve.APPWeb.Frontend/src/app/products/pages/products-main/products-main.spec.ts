import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ProductsMain } from './products-main';

describe('ProductsMain', () => {
  let component: ProductsMain;
  let fixture: ComponentFixture<ProductsMain>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ProductsMain]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ProductsMain);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
