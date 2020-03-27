import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AdminRoutingModule } from "./admin-routing.module";
import { QrWriterModule } from "src/app/components/qr-writer/qr-writer.module";

import { AdminComponent } from "./admin.component";
import { MainComponent } from "./main/main.component";
import { RegisterEmployeeComponent } from "./register-employee/register-employee.component";

@NgModule({
  declarations: [AdminComponent, MainComponent, RegisterEmployeeComponent],
  imports: [CommonModule, AdminRoutingModule, QrWriterModule]
})
export class AdminModule {}
