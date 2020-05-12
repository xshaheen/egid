import { BrowserModule } from "@angular/platform-browser";
import { NgModule, ErrorHandler } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";
import { AppComponent } from "./app.component";
import { MainModule } from "./views/main/main.module";
import { AdminModule } from "./views/admin/admin.module";
import { CivilAffairsModule } from "./views/civil-affairs/civil-affairs.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";
import { HttpClientModule, HTTP_INTERCEPTORS } from "@angular/common/http";
import { ManageModule } from "./views/manage/manage.module";
import { AppHttpInterceptor } from "./core/interceptor";
import { LoginDialogModule } from "./components/login-dialog/login-dialog.module";
import { ModalDialogModule } from "./components/modal-dialog/modal-dialog.module";
import { ConfirmDialogModule } from "./components/confirm-dialog/confirm-dialog.module";
import { AppErrorHandler } from "./core/error.handler";
import { ForbiddenModule } from "./views/forbidden/forbidden.module";
import { ReadQrDialogModule } from "./components/read-qr-dialog/read-qr-dialog-dialog.module";
import { WriteQrDialogModule } from "./components/write-qr-dialog/write-qr-dialog-dialog.module";
import { HealthModule } from "./views/health/health.module";

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    LoginDialogModule,
    ModalDialogModule,
    ConfirmDialogModule,
    ReadQrDialogModule,
    WriteQrDialogModule,
    MainModule,
    AdminModule,
    HealthModule,
    CivilAffairsModule,
    ManageModule,
    ForbiddenModule,
  ],
  declarations: [AppComponent],
  bootstrap: [AppComponent],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: AppHttpInterceptor, multi: true },
    { provide: ErrorHandler, useClass: AppErrorHandler },
  ],
})
export class AppModule {}
