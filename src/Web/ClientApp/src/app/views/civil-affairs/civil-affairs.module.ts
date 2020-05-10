import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MaterialModule } from "src/app/material.module";
import { CivilAffairsRoutingModule } from "./civil-affairs-routing.module";

import { CivilAffairsComponent } from "./civil-affairs.component";
import { RegisterCitizenComponent } from "./register-citizen/register-citizen.component";

@NgModule({
  declarations: [CivilAffairsComponent, RegisterCitizenComponent],
  imports: [CommonModule, MaterialModule, CivilAffairsRoutingModule],
})
export class CivilAffairsModule {}
