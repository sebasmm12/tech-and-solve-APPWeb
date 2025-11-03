import { Component } from '@angular/core';
import { MaterialModule } from '../../../shared/modules/material/material-module';
import { FlexLayoutModule } from '@angular/flex-layout';
import { ProductsList } from '../../components/products-list/products-list';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-products-main',
  imports: [MaterialModule, FlexLayoutModule, ProductsList, RouterLink],
  templateUrl: './products-main.html',
  styleUrl: './products-main.less',
})
export class ProductsMain {}
