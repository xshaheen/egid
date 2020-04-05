import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { CivilAffairsComponent } from "./civil-affairs.component";
import { RegisterCardComponent } from "./register-card/register-card.component";
import { MainComponent } from "./main/main.component";
import { RegisterCitizenComponent } from "./register-citizen/register-citizen.component";

const civilAffairsRoutes: Routes = [
  {
    path: "",
    component: CivilAffairsComponent,
    children: [
      { path: "", component: MainComponent, pathMatch: "full" },
      { path: "register", component: RegisterCitizenComponent },
      { path: "card", component: RegisterCardComponent },
    ],
  },
  { path: "**", redirectTo: "", pathMatch: "full" },
];

@NgModule({
  imports: [RouterModule.forChild(civilAffairsRoutes)],
  exports: [RouterModule],
})
export class CivilAffairsRoutingModule {}
