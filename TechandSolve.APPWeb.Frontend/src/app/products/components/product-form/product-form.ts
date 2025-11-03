import { Component, inject, input, OnInit, output } from '@angular/core';
import { MaterialModule } from '../../../shared/modules/material/material-module';
import { FlexLayoutModule } from '@angular/flex-layout';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ProductResponse } from '../../models/responses/product-response';

@Component({
  selector: 'app-product-form',
  imports: [MaterialModule, FlexLayoutModule, FormsModule, ReactiveFormsModule],
  templateUrl: './product-form.html',
  styleUrl: './product-form.less',
})
export class ProductForm implements OnInit {
  formBuilder = inject(FormBuilder);

  productInfo = input<ProductResponse>();
  save = output<any>();

  form!: FormGroup;

  ngOnInit() {
    this.buildForm();
  }

  buildForm() {
    this.form = this.formBuilder.group({
      id: [null],
      name: ['', [Validators.required, Validators.maxLength(100)]],
      description: ['', [Validators.required, Validators.maxLength(500)]],
      price: ['', [Validators.required, Validators.min(1)]],
      stock: ['', [Validators.required, Validators.min(0), Validators.pattern('^[0-9]*$')]],
    });

    this.setFormData(this.productInfo());
  }

  submit() {
    this.save.emit(this.form.value);
  }

  setFormData(productData?: ProductResponse) {
    if (!productData) return;

    this.form.patchValue({
      id: productData.id,
      name: productData.name,
      description: productData.description,
      price: productData.price,
      stock: productData.stock,
    });
  }
}
