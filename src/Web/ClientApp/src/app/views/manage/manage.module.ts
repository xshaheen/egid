import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MaterialModule } from "src/app/material.module";
import { ManageRoutingModule } from "./manage-routing.module";
import { FileModule } from "src/app/components/file/file.module";

import { ManageComponent } from "./manage.component";
import { CardComponent } from "./card/card.component";
import { SignDocComponent } from "./sign-doc/sign-doc.component";
import { VerifySignatureComponent } from "./verify-signature/verify-signature.component";
import { UpdateHealthInfoComponent } from "./update-health-info/update-health-info.component";
import { PasswordDialogModule } from "src/app/components/password-dialog/password-dialog.module";
import { FormsModule } from "@angular/forms";

@NgModule({
  declarations: [
    ManageComponent,
    CardComponent,
    SignDocComponent,
    VerifySignatureComponent,
    UpdateHealthInfoComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    MaterialModule,
    ManageRoutingModule,
    FileModule,
    PasswordDialogModule,
  ],
})
export class ManageModule {}
