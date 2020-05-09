import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MaterialModule } from "src/app/material.module";

import { ModalDialogComponent } from "./modal-dialog.component";

@NgModule({
  declarations: [ModalDialogComponent],
  imports: [CommonModule, FormsModule, MaterialModule],
  exports: [ModalDialogComponent],
})
export class ModalDialogModule {}
