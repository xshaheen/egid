import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { LoginDialogComponent } from "src/app/components/login-dialog/login-dialog.component";
import { AuthService } from "src/app/services/auth.service";

@Component({
  selector: "eg-main",
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"],
})
export class MainComponent {
  constructor(private dialog: MatDialog, public authService: AuthService) {}

  openLoginDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      width: "550px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
    });
  }
}
