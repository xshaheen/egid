import { Component, OnInit } from "@angular/core";
import { BreakpointsService } from "src/app/services/breakpoints.service";
import { LoginDialogComponent } from "src/app/components/login-dialog/login-dialog.component";
import { MatDialog } from "@angular/material/dialog";
import { AuthService } from "src/app/services/auth.service";

@Component({
  templateUrl: "./manage.component.html",
  styleUrls: ["./manage.component.scss"],
})
export class ManageComponent implements OnInit {
  isHandset: boolean;

  constructor(
    public authService: AuthService,
    private breakpoints: BreakpointsService,
    public dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.breakpoints
      .isHandset()
      .subscribe((result) => (this.isHandset = result));
  }

  openLoginDialog(): void {
    this.dialog.open(LoginDialogComponent, {
      width: "550px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
    });
  }
}
