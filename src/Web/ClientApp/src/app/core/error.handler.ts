import { ErrorHandler, Injectable, Injector } from "@angular/core";
import { AppModalService } from "../services/modal.service";

@Injectable({ providedIn: "root" })
export class AppErrorHandler implements ErrorHandler {
  constructor(private modal: AppModalService) {}
  handleError(error: any) {
    // ... log to logging server
    this.modal.showErrorSnackBar("حدث خطأ اثناء تنفيذ طلبك.");
    console.error("#AppErrorHandler detect an error:", error);
  }
}
