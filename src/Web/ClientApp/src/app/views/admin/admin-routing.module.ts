import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { EmployeesComponent } from "./employees/employees.component";
import { RegisterEmployeeComponent } from "./register-employee/register-employee.component";

const adminRoutes: Routes = [
  {
    path: "",
    component: AdminComponent,
    children: [
      { path: "", component: EmployeesComponent, pathMatch: "full" },
      { path: "employees", component: EmployeesComponent },
      { path: "register", component: RegisterEmployeeComponent },
    ],
  },
  { path: "**", redirectTo: "" },
];

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
