import { Component } from "@angular/core";
import { MatTableDataSource } from "@angular/material/table";
import { MatDialog } from "@angular/material/dialog";
import { ReadQrDialogComponent } from "src/app/components/read-qr-dialog/read-qr-dialog-dialog.component";
import { ConfirmDialogComponent } from "src/app/components/confirm-dialog/confirm-dialog.component";

@Component({
  templateUrl: "./employees.component.html",
  styleUrls: ["./employees.component.scss"],
})
export class EmployeesComponent {
  // constructor(private data: DataSource) {}
  // getEmployees(): MatTableDataSource<ICitizen> {
  //   const employees: ICitizen[] = [];
  //   this.data.getEmployees().subscribe((e) => employees.push(e));
  //   return new MatTableDataSource(employees);
  // }
  constructor(private readonly dialog: MatDialog) {}

  getQrCode() {
    const readDialog = this.dialog.open(ReadQrDialogComponent, {
      width: "450px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
    });
  }

  delete() {
    const confirmDialog = this.dialog.open(ConfirmDialogComponent, {
      width: "450px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
      data: "هل انت متاكد من حذف هذا الموظف؟",
    });
  }
}
