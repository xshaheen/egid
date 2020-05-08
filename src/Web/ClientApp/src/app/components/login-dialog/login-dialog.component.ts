import { Component } from "@angular/core";
import { MatDialogRef } from "@angular/material/dialog";
import { Router } from "@angular/router";
import { AuthService } from "src/app/services/auth.service";
import { ILoginCommand, LoginCommand } from "src/app/api";

@Component({
  templateUrl: "./login-dialog.component.html",
})
export class LoginDialogComponent {
  loginModel: ILoginCommand = {
    cardId: null,
    pin1: null,
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
    this.authService.signIn(new LoginCommand(this.loginModel));
    this.dialogRef.close();
  }
}
