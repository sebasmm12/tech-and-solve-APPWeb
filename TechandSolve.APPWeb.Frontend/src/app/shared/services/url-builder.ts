import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment.development';

@Injectable({
  providedIn: 'root',
})
export class UrlBuilder {
  protected url = '';

  for(segment: string): this {
    this.setUrlSlash();

    this.url = this.url.concat(`${segment}`);

    return this;
  }

  forProducts(): this {
    this.setUrlSlash();

    this.url = this.url.concat('products/');

    return this;
  }

  build(): string {
    try {
      return environment.baseUrl.concat(this.url);
    } finally {
      this.url = '';
    }
  }

  private setUrlSlash(): void {
    if (this.url?.length && !this.url.endsWith('/')) {
      this.url = this.url.concat('/');
    }
  }
}
