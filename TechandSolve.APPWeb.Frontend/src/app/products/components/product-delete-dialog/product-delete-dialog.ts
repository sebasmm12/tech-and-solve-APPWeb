import { Component, inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef, MatDialogContent } from '@angular/material/dialog';
import { MaterialModule } from '../../../shared/modules/material/material-module';

@Component({
  selector: 'app-product-delete-dialog',
  imports: [MaterialModule, MatDialogContent],
  templateUrl: './product-delete-dialog.html',
  styleUrl: './product-delete-dialog.less',
})
export class ProductDeleteDialog {
  data = inject<any>(MAT_DIALOG_DATA);
  dialogRef = inject(MatDialogRef<ProductDeleteDialog>);

  close() {
    this.dialogRef.close(false);
  }

  delete() {
    this.dialogRef.close(true);
  }
}
