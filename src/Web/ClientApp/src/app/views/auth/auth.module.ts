import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { QrReaderModule } from "src/app/components/qr-reader/qr-reader.module";

import { AuthComponent } from "./auth.component";

@NgModule({
  declarations: [AuthComponent],
  imports: [CommonModule, QrReaderModule],
})
export class AuthModule {}
