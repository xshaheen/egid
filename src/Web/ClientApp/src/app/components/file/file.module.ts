import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MaterialModule } from "src/app/material.module";

import { DropZoneDirective } from "src/app/core/directives/drop-zone.directive";

import { FileComponent } from "./file.component";

@NgModule({
  declarations: [FileComponent, DropZoneDirective],
  imports: [CommonModule, MaterialModule],
  exports: [FileComponent],
})
export class FileModule {}
