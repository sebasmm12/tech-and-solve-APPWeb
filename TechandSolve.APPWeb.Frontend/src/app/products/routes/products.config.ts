import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { routes } from './products.route';

export const productsConfig: ApplicationConfig = {
  providers: [provideRouter(routes)],
};
