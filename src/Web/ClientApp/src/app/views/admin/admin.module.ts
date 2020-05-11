import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";

import { AdminRoutingModule } from "./admin-routing.module";
import { MaterialModule } from "src/app/material.module";

import { AdminComponent } from "./admin.component";
import { EmployeesComponent } from "./employees/employees.component";

@NgModule({
  declarations: [AdminComponent, EmployeesComponent],
  imports: [CommonModule, MaterialModule, AdminRoutingModule],
})
export class AdminModule {}
