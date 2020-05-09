import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { HttpClientModule } from "@angular/common/http";
import { ManageRoutingModule } from "./manage-routing.module";
import { MaterialModule } from "src/app/material.module";
import { FileModule } from "src/app/components/file/file.module";

import { ManageComponent } from "./manage.component";
import { CardComponent } from "./card/card.component";
import { UpdateInfoComponent } from "./update-info/update-info.component";
import { SignDocComponent } from "./sign-doc/sign-doc.component";
import { VerifySignatureComponent } from "./verify-signature/verify-signature.component";
import { UpdateHealthInfoComponent } from "./update-health-info/update-health-info.component";
import { PasswordDialogModule } from "src/app/components/password-dialog/password-dialog.module";
import { ConfirmDialogModule } from "src/app/components/confirm-dialog/confirm-dialog.module";
import { LoginDialogModule } from "src/app/components/login-dialog/login-dialog.module";

@NgModule({
  declarations: [
    ManageComponent,
    CardComponent,
    UpdateInfoComponent,
    SignDocComponent,
    VerifySignatureComponent,
    UpdateHealthInfoComponent,
  ],
  imports: [
    CommonModule,
    ManageRoutingModule,
    MaterialModule,
    FileModule,
    PasswordDialogModule,
    ConfirmDialogModule,
  ],
})
export class ManageModule {}
