import { Component, DestroyRef, inject, OnInit } from '@angular/core';
import { ProductResponse } from '../../models/responses/product-response';
import { ProductsManagement } from '../../services/products-management';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { MaterialModule } from '../../../shared/modules/material/material-module';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialog } from '@angular/material/dialog';
import { ProductDeleteDialog } from '../product-delete-dialog/product-delete-dialog';

@Component({
  selector: 'app-products-list',
  imports: [MaterialModule, CommonModule],
  templateUrl: './products-list.html',
  styleUrl: './products-list.less',
})
export class ProductsList implements OnInit {
  destroyRef = inject(DestroyRef);
  dialog = inject(MatDialog);

  productsManagement = inject(ProductsManagement);

  router = inject(Router);
  route = inject(ActivatedRoute);

  products: ProductResponse[] = [];
  loading: boolean = false;

  productDisplayColumns: string[] = ['name', 'description', 'price', 'stock', 'actions'];

  ngOnInit(): void {
    this.getAll();
  }

  getAll(): void {
    this.loading = true;
    this.productsManagement
      .getAll()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe({
        next: (data) => {
          this.products = data;
          this.loading = false;
        },
        error: (error) => {
          console.error('Error fetching products:', error);
          this.loading = false;
        },
      });
  }

  edit(productId: number): void {
    this.router.navigate([`/products/edit/${productId}`], { relativeTo: this.route });
  }

  delete(product: ProductResponse): void {
    this.dialog
      .open(ProductDeleteDialog, {
        data: {
          name: product.name,
        },
      })
      .afterClosed()
      .pipe(takeUntilDestroyed(this.destroyRef))
      .subscribe((confirmed: boolean) => {
        if (!confirmed) return;

        this.productsManagement
          .delete(product.id)
          .pipe(takeUntilDestroyed(this.destroyRef))
          .subscribe({
            next: () => {
              this.getAll();
            },
            error: (error) => {
              console.error('Error deleting product:', error);
            },
          });
      });
  }
}
