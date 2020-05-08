import { Component } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { SignInModel, AuthService } from "src/app/services/auth.service";

@Component({
  templateUrl: "./login-dialog.component.html",
})
export class LoginDialogComponent {
  loginModel: SignInModel = {
    pin1: null,
    cardId: null,
  };

  constructor(
    public dialogRef: MatDialogRef<LoginDialogComponent>,
    private router: Router,
    private authService: AuthService
  ) {}

  public get isValid(): boolean {
    const valid =
      this.loginModel.pin1 != null && this.loginModel.cardId != null;
    return valid;
  }

  onClickCancel(): void {
    this.dialogRef.close();
    this.router.navigateByUrl("/");
  }

  onGetLoginString(result: string) {
    this.loginModel.cardId = result;
  }

  signIn() {
    this.authService.signIn(this.loginModel);
    this.dialogRef.close();
  }
}
