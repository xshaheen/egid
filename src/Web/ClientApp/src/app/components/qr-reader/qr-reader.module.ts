import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ZXingScannerModule } from "@zxing/ngx-scanner";
import { FormsModule } from "@angular/forms";
import { MaterialModule } from "src/app/material.module";

import { AppInfoDialogComponent } from "./app-info-dialog/app-info-dialog.component";

import { QrReaderComponent } from "./qr-reader.component";
import { AppInfoComponent } from "./app-info/app-info.component";

@NgModule({
  declarations: [QrReaderComponent, AppInfoComponent, AppInfoDialogComponent],
  imports: [CommonModule, ZXingScannerModule, FormsModule, MaterialModule],
  exports: [QrReaderComponent],
})
export class QrReaderModule {}
