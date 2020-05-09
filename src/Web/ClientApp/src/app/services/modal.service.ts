import { Injectable } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import { ModalDialogComponent } from "../components/modal-dialog/modal-dialog.component";

@Injectable({ providedIn: "root" })
export class AppModalService {
  constructor(private dialog: MatDialog) {}

  alert(message: string): void {
    this.dialog.open(ModalDialogComponent, {
      width: "450px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
      data: message,
    });
  }
}
