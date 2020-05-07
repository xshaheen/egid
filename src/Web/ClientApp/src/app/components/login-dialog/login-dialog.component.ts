import { Component, Inject } from "@angular/core";
import { MatDialogRef, MAT_DIALOG_DATA } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { SignInModel } from "src/app/services/auth.service";
import { createWriteStream } from "fs";

@Component({
  templateUrl: "./login-dialog.component.html",
})
export class LoginDialogComponent {
  loginModel: SignInModel = {
    pin1: null,
    privateKey: null,
  };

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    private router: Router
  ) {}

  public get isValid(): boolean {
    const valid =
      this.loginModel.pin1 != null && this.loginModel.privateKey != null;
    return valid;
  }

  onClickCancel(): void {
    this.dialogRef.close();
    this.router.navigateByUrl("/");
  }

  onGetLoginString(result: string) {
    this.loginModel.privateKey = result;
  }
}
