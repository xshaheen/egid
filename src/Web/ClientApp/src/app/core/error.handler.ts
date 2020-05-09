import { HttpErrorResponse } from "@angular/common/http";
import { ErrorHandler, Injectable, Injector } from "@angular/core";
import { AppModalService } from "../services/modal.service";
import { LoginService } from "../services/login.service";
import { ApiException } from "../api";

@Injectable({ providedIn: "root" })
export class AppErrorHandler implements ErrorHandler {
  constructor(private readonly injector: Injector) {}

  handleError(error: any) {
    if (error instanceof HttpErrorResponse) {
      switch (error.status) {
        case 401: {
          const loginService = this.injector.get<LoginService>(LoginService);
          loginService.login();
          return;
        }
      }
    } else if (error instanceof ApiException) {
      switch (error.status) {
        case 404: {
          // NOT FOUNDED - server put message in the response details
          console.log("not founded exception occurs", error);
          const appModalService = this.injector.get<AppModalService>(
            AppModalService
          );
          appModalService.alert(error.response);
          return;
        }
        case 400: {
          // BAD REQUEST - server put message in the response details
          console.log("Bad request exception occurs", error);
          const appModalService = this.injector.get<AppModalService>(
            AppModalService
          );
          appModalService.alert(error.response);
          return;
        }
        case 500: {
          // Internal error
          const appModalService = this.injector.get<AppModalService>(
            AppModalService
          );
          appModalService.alert(
            "خطأ في السرفر تم تسجيل الخطأ وجاري العمل عليه."
          );
          return;
        }
      }
    }

    console.error(error);
  }
}
