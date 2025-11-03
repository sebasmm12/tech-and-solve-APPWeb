import { Component, DestroyRef, inject } from '@angular/core';
import { ProductForm } from '../../components/product-form/product-form';
import { ProductRegisterRequest } from '../../models/requests/product-register-request';
import { ProductsManagement } from '../../services/products-management';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-product-register',
  imports: [ProductForm],
  templateUrl: './product-register.html',
  styleUrl: './product-register.less',
})
export class ProductRegister {
  destroyRef = inject(DestroyRef);
  productsManagement = inject(ProductsManagement);
  router = inject(Router);
  route = inject(ActivatedRoute);

  create(productRegisterRequest: ProductRegisterRequest): void {
    this.productsManagement
      .create(productRegisterRequest)
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (_) => {
          this.router.navigate(['../'], { relativeTo: this.route });
        },
        error: (error) => {
          console.error('Error creating product:', error);
        },
      });
  }
}
