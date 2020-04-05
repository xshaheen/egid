import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";

export interface IPasswordDialogData {
  passwordType: string;
}

@Component({
  selector: "eg-password-dialog",
  templateUrl: "./password-dialog.component.html",
})
export class PasswordDialogComponent {
  password: string;

  constructor(
    public dialogRef: MatDialogRef<PasswordDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: IPasswordDialogData
  ) {}

  onClickCancel(): void {
    this.dialogRef.close();
  }
}
