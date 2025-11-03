import { Routes } from '@angular/router';

export const routes: Routes = [
  {
    path: 'products',
    loadChildren: () => import('./products/routes/products.route').then((m) => m.routes),
  },
];
