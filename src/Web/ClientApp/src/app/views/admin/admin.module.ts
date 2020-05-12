import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AdminRoutingModule } from "./admin-routing.module";
import { MaterialModule } from "src/app/material.module";

import { AdminComponent } from "./admin.component";
import { EmployeesComponent } from "./employees/employees.component";
import { ReadQrDialogModule } from "src/app/components/read-qr-dialog/read-qr-dialog-dialog.module";

@NgModule({
  declarations: [AdminComponent, EmployeesComponent],
  imports: [
    CommonModule,
    MaterialModule,
    AdminRoutingModule,
    ReadQrDialogModule,
  ],
})
export class AdminModule {}
