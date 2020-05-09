import { Injectable } from "@angular/core";
import { ProblemDetails, ApiException } from "../api";
import { AppModalService } from "../services/modal.service";

@Injectable({ providedIn: "root" })
export class ErrorService {
  constructor(private readonly modal: AppModalService) {}

  handleError(error: any): void {
    if (error instanceof ProblemDetails) {
      this.modal.showErrorSnackBar(error.detail);
      return;
    } else if (error instanceof ApiException) {
      switch (error.status) {
        case 404: {
          console.log("#ERROR 404#", error);
          // NOT FOUNDED
          this.modal.showErrorSnackBar(error.response);
          return;
        }
        case 400: {
          // BAD REQUEST
          console.log("#ERROR 400#", error);
          this.modal.showErrorSnackBar(error.message);
          return;
        }
        case 500: {
          console.log("#ERROR 500#", error);
          // Internal error
          this.modal.showErrorSnackBar(
            "خطأ في السرفر تم تسجيل الخطأ وجاري العمل عليه."
          );
          return;
        }
      }
    }

    console.error("#ERROR#", error);
  }
}
