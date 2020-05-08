import { Injectable } from "@angular/core";
import { CanActivate, Router } from "@angular/router";
import { AppTokenService } from "../services/token.service";
import { MatDialog } from "@angular/material/dialog";
import { LoginDialogComponent } from "../components/login-dialog/login-dialog.component";

@Injectable({ providedIn: "root" })
export class AuthGuard implements CanActivate {
  constructor(
    private readonly router: Router,
    private readonly appTokenService: AppTokenService,
    public dialog: MatDialog
  ) {}

  canActivate() {
    if (this.appTokenService.any()) {
      return true;
    }
    this.openLoginDialog();
    return false;
  }

  openLoginDialog(): void {
    const dialogRef = this.dialog.open(LoginDialogComponent, {
      width: "550px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
    });
  }
}
