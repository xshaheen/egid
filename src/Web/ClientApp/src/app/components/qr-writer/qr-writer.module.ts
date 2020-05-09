import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { QRCodeModule } from "angularx-qrcode";
import { MaterialModule } from "src/app/material.module";

import { QrWriterComponent } from "./qr-writer.component";

@NgModule({
  declarations: [QrWriterComponent],
  imports: [CommonModule, QRCodeModule, MaterialModule],
  exports: [QrWriterComponent],
})
export class QrWriterModule {}
