import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { MaterialModule } from "src/app/material.module";
import { QrReaderModule } from "../qr-reader/qr-reader.module";
import { ReadQrDialogComponent } from "./read-qr-dialog-dialog.component";

@NgModule({
  declarations: [ReadQrDialogComponent],
  imports: [CommonModule, FormsModule, MaterialModule, QrReaderModule],
  exports: [ReadQrDialogComponent],
})
export class ReadQrDialogModule {}
