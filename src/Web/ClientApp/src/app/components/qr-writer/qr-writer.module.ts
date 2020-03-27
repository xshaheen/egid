import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { QRCodeModule } from "angularx-qrcode";

import { QrWriterComponent } from "./qr-writer.component";

@NgModule({
  declarations: [QrWriterComponent],
  imports: [CommonModule, QRCodeModule],
  exports: [QrWriterComponent]
})
export class QrWriterModule {}
