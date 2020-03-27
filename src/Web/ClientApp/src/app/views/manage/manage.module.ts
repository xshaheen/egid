import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { ManageRoutingModule } from "./manage-routing.module";

import { ManageComponent } from "./manage.component";
import { ChangePinComponent } from "./change-pin/change-pin.component";
import { UpdateInfoComponent } from "./update-info/update-info.component";
import { SignDocComponent } from "./sign-doc/sign-doc.component";
import { VerifySignatureComponent } from "./verify-signature/verify-signature.component";
import { UpdateHealthInfoComponent } from "./update-health-info/update-health-info.component";
import { FileModule } from "src/app/components/file/file.module";

@NgModule({
  declarations: [
    ManageComponent,
    ChangePinComponent,
    UpdateInfoComponent,
    SignDocComponent,
    VerifySignatureComponent,
    UpdateHealthInfoComponent
  ],
  imports: [CommonModule, ManageRoutingModule, FileModule]
})
export class ManageModule {}
