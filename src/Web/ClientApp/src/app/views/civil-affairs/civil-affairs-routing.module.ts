import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";

import { CivilAffairsComponent } from "./civil-affairs.component";
import { RegisterCardComponent } from "./register-card/register-card.component";
import { ReviewUpdateRequestsComponent } from "./review-update-requests/review-update-requests.component";

const civilAffairsRoutes: Routes = [
  {
    path: "",
    component: CivilAffairsComponent,
    children: [
      { path: "register", component: RegisterCardComponent },
      { path: "requests", component: ReviewUpdateRequestsComponent }
    ]
  },
  { path: "**", redirectTo: "" }
];

@NgModule({
  imports: [RouterModule.forChild(civilAffairsRoutes)],
  exports: [RouterModule]
})
export class CivilAffairsRoutingModule {}
