import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MaterialModule } from "src/app/material.module";

import { OneFileComponent } from "./one-file.component";

@NgModule({
  declarations: [OneFileComponent],
  imports: [CommonModule, MaterialModule],
  exports: [OneFileComponent],
})
export class OneFileModule {}
