import { Component } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";

@Component({
  templateUrl: "./read-qr-dialog.component.html",
})
export class ReadQrDialogComponent {
  result: string;

  constructor(public dialogRef: MatDialogRef<ReadQrDialogComponent>) {}

  cancel() {
    this.dialogRef.close(null);
  }

  onQrSuccess(result: string) {
    this.result = result;
    this.dialogRef.close(this.result);
  }
}
