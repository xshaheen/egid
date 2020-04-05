import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { MaterialModule } from "src/app/material.module";
import { AdminRoutingModule } from "./admin-routing.module";
import { QrWriterModule } from "src/app/components/qr-writer/qr-writer.module";

import { AdminComponent } from "./admin.component";
import { MainComponent } from "./main/main.component";
import { RegisterEmployeeComponent } from "./register-employee/register-employee.component";
import { EmployeesComponent } from "./employees/employees.component";

@NgModule({
  declarations: [
    AdminComponent,
    MainComponent,
    RegisterEmployeeComponent,
    EmployeesComponent,
  ],
  imports: [CommonModule, MaterialModule, AdminRoutingModule, QrWriterModule],
})
export class AdminModule {}
