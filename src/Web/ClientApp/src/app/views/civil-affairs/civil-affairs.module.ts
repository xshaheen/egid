import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MaterialModule } from "src/app/material.module";
import { CivilAffairsRoutingModule } from "./civil-affairs-routing.module";
import { FormsModule } from "@angular/forms";
import { OneFileModule } from "src/app/components/one-file/one-file.module";
import { QrWriterModule } from "src/app/components/qr-writer/qr-writer.module";

import { CivilAffairsComponent } from "./civil-affairs.component";
import { RegisterCitizenComponent } from "./register-citizen/register-citizen.component";

@NgModule({
  declarations: [CivilAffairsComponent, RegisterCitizenComponent],
  imports: [
    CommonModule,
    MaterialModule,
    FormsModule,
    CivilAffairsRoutingModule,
    OneFileModule,
    QrWriterModule,
  ],
})
export class CivilAffairsModule {}
