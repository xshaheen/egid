import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

class WriteQrCommand {
  str: string;
  title: string;
}

@Component({
  templateUrl: "./write-qr-dialog.component.html",
})
export class WriteQrDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<WriteQrDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: WriteQrCommand
  ) {}

  cancel() {
    this.dialogRef.close(null);
  }
}
