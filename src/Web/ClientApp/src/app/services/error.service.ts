import { Injectable } from "@angular/core";
import { ProblemDetails, ApiException } from "../api";
import { AppModalService } from "../services/modal.service";

@Injectable({ providedIn: "root" })
export class ErrorService {
  constructor(private readonly modal: AppModalService) {}

  handleError(error: any) {
    if (error instanceof ProblemDetails) {
      this.modal.showErrorSnackBar(error.detail);
      return;
    } else if (error instanceof ApiException) {
      switch (error.status) {
        case 404: {
          // NOT FOUNDED
          this.modal.showErrorSnackBar(error.response);
          return;
        }
        case 400: {
          // BAD REQUEST
          this.modal.showErrorSnackBar(error.response);
          return;
        }
        case 500: {
          // Internal error
          this.modal.showErrorSnackBar(
            "خطأ في السرفر تم تسجيل الخطأ وجاري العمل عليه."
          );
          return;
        }
      }
    }
    console.log("#ERROR#");

    console.error(error);
    return;
  }
}
