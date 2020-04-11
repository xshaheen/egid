import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MaterialModule } from "src/app/material.module";

import { ConfirmDialogComponent } from "./confirm-dialog.component";

@NgModule({
  declarations: [ConfirmDialogComponent],
  imports: [CommonModule, FormsModule, MaterialModule],
  exports: [ConfirmDialogComponent],
})
export class ConfirmDialogModule {}
