import { ResolveFn } from '@angular/router';
import { ProductsManagement } from '../services/products-management';
import { DestroyRef, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { ProductResponse } from '../models/responses/product-response';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

export const productInfoResolver: ResolveFn<Observable<ProductResponse>> = (route, state) => {
  const destroyRef = inject(DestroyRef);
  const productsManagement = inject(ProductsManagement);

  const id = Number(route.paramMap.get('id'));

  return productsManagement.get(id).pipe(takeUntilDestroyed(destroyRef));
};
