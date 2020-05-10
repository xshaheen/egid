import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { CivilAffairsComponent } from "./civil-affairs.component";
import { RegisterCitizenComponent } from "./register-citizen/register-citizen.component";

const civilAffairsRoutes: Routes = [
  {
    path: "",
    component: CivilAffairsComponent,
    children: [
      { path: "", component: RegisterCitizenComponent, pathMatch: "full" },
      { path: "register", component: RegisterCitizenComponent },
    ],
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];

@NgModule({
  imports: [RouterModule.forChild(civilAffairsRoutes)],
  exports: [RouterModule],
})
export class CivilAffairsRoutingModule {}
