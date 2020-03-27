import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FileComponent } from "./file.component";

import { DropZoneDirective } from "src/app/core/directives/drop-zone.directive";

@NgModule({
  declarations: [FileComponent, DropZoneDirective],
  imports: [CommonModule],
  exports: [FileComponent]
})
export class FileModule {}
