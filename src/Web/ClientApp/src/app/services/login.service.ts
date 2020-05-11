import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { MatDialog } from "@angular/material/dialog";
import { AuthService } from "./auth.service";
import {
  LoginDialogComponent,
  LoginDialogResult,
} from "../components/login-dialog/login-dialog.component";
import { LoginCommand } from "../api";
import { ErrorService } from "./error.service";

@Injectable({ providedIn: "root" })
export class LoginService {
  constructor(
    private readonly dialog: MatDialog,
    private readonly router: Router,
    private readonly authService: AuthService,
    private readonly errorHandler: ErrorService
  ) {}

  login(returnUrl?: string): void {
    // Open login dialog
    const dialogRef = this.dialog.open(LoginDialogComponent, {
      width: "550px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
    });

    // What to close and try to login
    dialogRef.afterClosed().subscribe((result: LoginDialogResult) => {
      if (!result || result.isCanceled) {
        this.router.navigateByUrl("/");
      } else {
        this.authService
          .signIn(
            new LoginCommand({ cardId: result.cardId, pin1: result.pin1 })
          )
          .subscribe(
            () => {
              this.router.navigateByUrl("/");
              this.router.navigateByUrl(returnUrl);
            },
            (err) => this.errorHandler.handleError(err)
          );
      }
    });
  }
}
