import { Injectable } from "@angular/core";

@Injectable({
  providedIn: "root"
})
export class LoggingService {
  logError(error: string): void {
    console.log(`Error: ${error}`);
  }

  logInfo(msg: string): void {
    console.log(`Info: ${msg}`);
  }
}
