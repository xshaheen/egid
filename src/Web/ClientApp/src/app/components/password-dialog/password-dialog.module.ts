import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MaterialModule } from "src/app/material.module";

import { PasswordDialogComponent } from "./password-dialog.component";

@NgModule({
  declarations: [PasswordDialogComponent],
  imports: [CommonModule, FormsModule, MaterialModule],
  exports: [PasswordDialogComponent],
})
export class PasswordDialogModule {}
