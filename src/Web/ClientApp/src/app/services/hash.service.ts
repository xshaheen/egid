/* tslint:disable */
import { Injectable } from "@angular/core";
import { Observable, from, of, throwError } from "rxjs";
import { switchMap, catchError } from "rxjs/operators";
import { encode } from "base64-arraybuffer";
import { ErrorService } from "./error.service";

@Injectable({
  providedIn: "root",
})
export class HashService {
  constructor(private readonly errorService: ErrorService) {}

  /**
   * Returns SHA-512 Hash of the file as Base64 string format
   * @param buffer file array buffer
   */
  hash(file: File): Observable<string> {
    return this.fileToArrayBuffer(file).pipe(
      switchMap((buffer) =>
        this.hashArrayBuffer(buffer).pipe(
          switchMap((hash) => {
            const base64 = encode(hash);
            return [base64];
          })
        )
      )
    );
  }

  private hashArrayBuffer(buff: ArrayBuffer): Observable<ArrayBuffer> {
    return from(crypto.subtle.digest("SHA-512", buff));
  }

  private fileToArrayBuffer(file: File): Observable<ArrayBuffer> {
    return from(this.fileToArrayBufferPromise(file));
  }

  private fileToArrayBufferPromise(file: File): Promise<ArrayBuffer> {
    return new Promise(function (resolve, reject) {
      const reader = new FileReader();

      reader.onerror = function onerror(ev) {
        reject(ev.target.error);
      };

      reader.onload = function onload(ev) {
        resolve(ev.target.result as ArrayBuffer);
      };
      reader.readAsArrayBuffer(file);
    });
  }

  private toBase64(buffer: ArrayBuffer): string {
    // transform to string
    const str = new Uint8Array(buffer).reduce(
      (accumulator, current) => accumulator + String.fromCharCode(current),
      ""
    );
    // btoa() -- encodes a string in base-64.
    return btoa(str);
  }
}
