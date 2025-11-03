import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { ProductsManagement } from '../../services/products-management';
import { ProductResponse } from '../../models/responses/product-response';
import { ActivatedRoute, Router } from '@angular/router';
import { ProductForm } from '../../components/product-form/product-form';
import { ProductUpdateRequest } from '../../models/requests/product-update-request';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  selector: 'app-product-update',
  imports: [ProductForm],
  templateUrl: './product-update.html',
  styleUrl: './product-update.less',
})
export class ProductUpdate implements OnInit {
  destroyRef = inject(DestroyRef);
  productsManagement = inject(ProductsManagement);
  router = inject(Router);
  route = inject(ActivatedRoute);

  productInfo!: ProductResponse;

  ngOnInit(): void {
    this.productInfo = this.route.snapshot.data['product'];
  }

  update(productUpdateRequest: ProductUpdateRequest): void {
    this.productsManagement
      .update(productUpdateRequest)
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
