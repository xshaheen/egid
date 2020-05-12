import { Component } from "@angular/core";
import { WriteQrDialogComponent } from "src/app/components/write-qr-dialog/write-qr-dialog-dialog.component";
import { MatDialog } from "@angular/material/dialog";

interface Food {
  value: string;
  viewValue: string;
}

@Component({
  selector: "eg-register-citizen",
  templateUrl: "./register-citizen.component.html",
  styleUrls: ["./register-citizen.component.scss"],
})
export class RegisterCitizenComponent {
  favoriteSeason: string;
  seasons: string[] = ["Winter", "Spring", "Summer", "Autumn"];

  constructor(private readonly dialog: MatDialog) {}

  foods: Food[] = [
    { value: "steak-0", viewValue: "Steak" },
    { value: "pizza-1", viewValue: "Pizza" },
    { value: "tacos-2", viewValue: "Tacos" },
  ];

  openWriteQrDialog() {
    const readDialog = this.dialog.open(WriteQrDialogComponent, {
      width: "450px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
    });
  }
}
