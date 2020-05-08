import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { MainComponent } from "./main/main.component";
import { RegisterEmployeeComponent } from "./register-employee/register-employee.component";
// import { EmployeesComponent } from "./employees/employees.component";

const adminRoutes: Routes = [
  {
    path: "",
    component: AdminComponent,
    children: [
      { path: "", component: MainComponent },
      // { path: "employees", component: EmployeesComponent },
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
