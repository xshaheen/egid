import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { MatSnackBar } from "@angular/material/snack-bar";
import { ModalDialogComponent } from "../components/modal-dialog/modal-dialog.component";

@Injectable({ providedIn: "root" })
export class AppModalService {
  constructor(public dialog: MatDialog, public snackBar: MatSnackBar) {}

  alertDialog(message: string): void {
    this.dialog.open(ModalDialogComponent, {
      data: message,
      width: "450px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
    });
  }

  showSuccessSnackBar(message: string): void {
    this.snackBar.open(message, null, {
      direction: "rtl",
      panelClass: ["snack-success"],
      duration: 5000,
    });
  }

  showErrorSnackBar(message: string): void {
    this.snackBar.open(message, "X", {
      direction: "rtl",
      panelClass: ["snack-success"],
      duration: 10000,
    });
  }

  showNormalSnackBar(message: string): void {
    this.snackBar.open(message, null, { direction: "rtl" });
  }
}
