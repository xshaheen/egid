import { Injectable } from "@angular/core";
import { Observable, from } from "rxjs";
import { map } from "rxjs/operators";

@Injectable({
  providedIn: "root"
})
export class HashService {
  toBuffer(file: File): Observable<ArrayBuffer> {
    return from(file.arrayBuffer());
  }

  hash(buffer: ArrayBuffer): Observable<string> {
    return from(crypto.subtle.digest("SHA-512", buffer)).pipe(
      map(buff => this.toBase64(buff))
    );
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
