import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { MatDialog } from "@angular/material/dialog";
import { AuthService } from "./auth.service";
import { LoginDialogComponent } from "../components/login-dialog/login-dialog.component";

@Injectable({ providedIn: "root" })
export class LoginService {
  constructor(
    private readonly dialog: MatDialog,
    private readonly router: Router,
    private readonly authService: AuthService
  ) {}

  login(returnUrl?: string): void {
    const dialogRef = this.dialog.open(LoginDialogComponent, {
      width: "550px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
    });

    // after close try to navigate again
    dialogRef.afterClosed().subscribe(() => {
      if (returnUrl && this.authService.isAuthenticated) {
        this.router.navigateByUrl(returnUrl);
      } else {
        this.router.navigateByUrl("/");
      }
    });
  }
}
