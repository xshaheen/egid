import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { CivilAffairsRoutingModule } from "./civil-affairs-routing.module";

import { CivilAffairsComponent } from "./civil-affairs.component";

@NgModule({
  declarations: [CivilAffairsComponent],
  imports: [CommonModule, CivilAffairsRoutingModule]
})
export class CivilAffairsModule {}
