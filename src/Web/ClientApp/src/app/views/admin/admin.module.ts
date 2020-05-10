import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AdminRoutingModule } from "./admin-routing.module";
import { QrWriterModule } from "src/app/components/qr-writer/qr-writer.module";
import { MaterialModule } from "src/app/material.module";

import { AdminComponent } from "./admin.component";
import { EmployeesComponent } from "./employees/employees.component";
import { RegisterEmployeeComponent } from "./register-employee/register-employee.component";

@NgModule({
  declarations: [AdminComponent, RegisterEmployeeComponent, EmployeesComponent],
  imports: [CommonModule, MaterialModule, AdminRoutingModule, QrWriterModule],
})
export class AdminModule {}
