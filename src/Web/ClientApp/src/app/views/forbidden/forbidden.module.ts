import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RouterModule } from "@angular/router";
import { MaterialModule } from "src/app/material.module";

import { ForbiddenComponent } from "./forbidden.component";

@NgModule({
  declarations: [ForbiddenComponent],
  imports: [CommonModule, RouterModule, MaterialModule],
})
export class ForbiddenModule {}
