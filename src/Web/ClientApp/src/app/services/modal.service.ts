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
    this.snackBar.open(message, null, { direction: "rtl" });
  }

  showErrorSnackBar(message: string): void {
    this.snackBar.open(message, "X", {
      panelClass: ["error"],
      direction: "rtl",
    });
  }
}
