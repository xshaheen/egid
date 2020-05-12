import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MaterialModule } from "src/app/material.module";
import { QrWriterModule } from "../qr-writer/qr-writer.module";
import { WriteQrDialogComponent } from "./write-qr-dialog-dialog.component";

@NgModule({
  declarations: [WriteQrDialogComponent],
  imports: [CommonModule, FormsModule, MaterialModule, QrWriterModule],
  exports: [WriteQrDialogComponent],
})
export class WriteQrDialogModule {}
