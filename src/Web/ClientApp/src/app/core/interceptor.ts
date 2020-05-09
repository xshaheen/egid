import {
  HttpHandler,
  HttpInterceptor,
  HttpRequest,
  HttpErrorResponse,
} from "@angular/common/http";

import { Injectable } from "@angular/core";
import { AppTokenService } from "../services/token.service";
import { LoginService } from "../services/login.service";
import { AppModalService } from "../services/modal.service";
import { catchError, retry } from "rxjs/operators";
import { throwError } from "rxjs";

@Injectable({ providedIn: "root" })
export class AppHttpInterceptor implements HttpInterceptor {
  constructor(
    private readonly appTokenService: AppTokenService,
    private readonly loginService: LoginService,
    private readonly modal: AppModalService
  ) {}

  intercept(request: HttpRequest<any>, next: HttpHandler) {
    request = request.clone({
      setHeaders: { Authorization: `Bearer ${this.appTokenService.get()}` },
    });

    return next.handle(request).pipe(
      retry(1),
      catchError((error: HttpErrorResponse) => {
        // Handle Unauthorized response
        if (error.status === 401) {
          this.modal.showErrorSnackBar(
            "الجلسة انتهم من فضلك اعد تسجيل الدخول للاستمرار"
          );
          this.loginService.login();
          return next.handle(request);
        }
        // Other error throw it the API client will handle it
        return throwError(error);
      })
    );
  }
}
