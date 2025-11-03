import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-products-container',
  imports: [RouterOutlet],
  templateUrl: './products-container.html',
  styleUrl: './products-container.less',
})
export class ProductsContainer {}
