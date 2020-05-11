import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { EmployeesComponent } from "./employees/employees.component";

const adminRoutes: Routes = [
  {
    path: "",
    component: AdminComponent,
    children: [
      { path: "", component: EmployeesComponent, pathMatch: "full" },
      { path: "employees", component: EmployeesComponent },
    ],
  },
  { path: "**", redirectTo: "" },
];

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule],
})
export class AdminRoutingModule {}
