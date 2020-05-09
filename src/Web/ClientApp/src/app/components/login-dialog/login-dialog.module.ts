import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MaterialModule } from "src/app/material.module";
import { QrReaderModule } from "../qr-reader/qr-reader.module";

import { LoginDialogComponent } from "./login-dialog.component";

@NgModule({
  declarations: [LoginDialogComponent],
  imports: [CommonModule, FormsModule, MaterialModule, QrReaderModule],
  exports: [LoginDialogComponent],
})
export class LoginDialogModule {}
