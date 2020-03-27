import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AuthComponent } from "./auth.component";
import { QrReaderComponent } from "src/app/components/qr-reader/qr-reader.component";

@NgModule({
  declarations: [AuthComponent, QrReaderComponent],
  imports: [CommonModule]
})
export class AuthModule {}
