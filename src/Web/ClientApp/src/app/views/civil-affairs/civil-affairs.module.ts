import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MaterialModule } from "src/app/material.module";
import { CivilAffairsRoutingModule } from "./civil-affairs-routing.module";

import { CivilAffairsComponent } from "./civil-affairs.component";
import { RegisterCitizenComponent } from "./register-citizen/register-citizen.component";
import { DeathCertificateComponent } from "./death-certificate/death-certificate.component";

@NgModule({
  declarations: [
    CivilAffairsComponent,
    RegisterCitizenComponent,
    DeathCertificateComponent,
  ],
  imports: [CommonModule, MaterialModule, CivilAffairsRoutingModule],
})
export class CivilAffairsModule {}
