import { Component } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";

export interface LoginDialogResult {
  isCanceled: boolean;
  cardId: string;
  pin1: string;
}

@Component({
  templateUrl: "./login-dialog.component.html",
})
export class LoginDialogComponent {
  result: LoginDialogResult = {
    isCanceled: true,
    cardId: null,
    pin1: null,
  };

  constructor(public dialogRef: MatDialogRef<LoginDialogComponent>) {}

  ok() {
    this.result.isCanceled = false;
    this.dialogRef.close(this.result);
  }

  cancel() {
    this.result.isCanceled = true;
    this.dialogRef.close(this.result);
  }

  onQrSuccess(result: string) {
    this.result.cardId = result;
  }

  public get isValid(): boolean {
    return this.result.pin1 != null && this.result.cardId != null;
  }
}
