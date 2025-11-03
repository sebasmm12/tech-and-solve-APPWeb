import { Routes } from '@angular/router';
import { ProductsContainer } from '../pages/products-container/products-container';
import { productInfoResolver } from '../resolvers/product-info-resolver';

export const routes: Routes = [
  {
    path: '',
    component: ProductsContainer,
    children: [
      {
        path: '',
        loadComponent: () =>
          import('../pages/products-main/products-main').then((m) => m.ProductsMain),
      },
      {
        path: 'create',
        loadComponent: () =>
          import('../pages/product-register/product-register').then((m) => m.ProductRegister),
      },
      {
        path: 'edit/:id',
        loadComponent: () =>
          import('../pages/product-update/product-update').then((m) => m.ProductUpdate),
        resolve: {
          product: productInfoResolver,
        },
      },
      {
        path: '**',
        redirectTo: '',
      },
    ],
  },
];
