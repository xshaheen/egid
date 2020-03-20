import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { AdminComponent } from "./admin.component";
import { MainComponent } from "./main/main.component";
import { RegisterEmployeeComponent } from "./register-employee/register-employee.component";

const adminRoutes: Routes = [
  {
    path: "",
    component: AdminComponent,
    children: [
      { path: "", component: MainComponent },
      { path: "register", component: RegisterEmployeeComponent }
    ]
  },
  { path: "**", redirectTo: "" }
];

@NgModule({
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule]
})
export class AdminRoutingModule {}
