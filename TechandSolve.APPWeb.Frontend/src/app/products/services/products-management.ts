import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { UrlBuilder } from '../../shared/services/url-builder';
import { Observable } from 'rxjs';
import { ProductResponse } from '../models/responses/product-response';
import { ProductRegisterRequest } from '../models/requests/product-register-request';
import { ProductUpdateRequest } from '../models/requests/product-update-request';

@Injectable({
  providedIn: 'root',
})
export class ProductsManagement {
  httpClient = inject(HttpClient);
  urlBuilder = inject(UrlBuilder);

  getAll(): Observable<ProductResponse[]> {
    const url = this.urlBuilder.forProducts().build();

    return this.httpClient.get<ProductResponse[]>(url);
  }

  get(id: number): Observable<ProductResponse> {
    const url = this.urlBuilder.forProducts().for(id.toString()).build();

    return this.httpClient.get<ProductResponse>(url);
  }

  create(request: ProductRegisterRequest): Observable<number> {
    const url = this.urlBuilder.forProducts().build();

    return this.httpClient.post<number>(url, request);
  }

  update(request: ProductUpdateRequest): Observable<void> {
    const url = this.urlBuilder.forProducts().build();

    return this.httpClient.put<void>(url, request);
  }

  delete(id: number): Observable<void> {
    const url = this.urlBuilder.forProducts().for(id.toString()).build();
    return this.httpClient.delete<void>(url);
  }
}
