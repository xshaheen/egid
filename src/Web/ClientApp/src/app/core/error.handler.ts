import { ErrorHandler, Injectable, Injector } from "@angular/core";

@Injectable({ providedIn: "root" })
export class AppErrorHandler implements ErrorHandler {
  handleError(error: any) {
    // ... log to logging server

    console.error("#ERROR#", error);
  }
}
