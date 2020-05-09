import { Injectable } from "@angular/core";
import { Observable, from, of } from "rxjs";
import { map, mergeMap } from "rxjs/operators";

@Injectable({
  providedIn: "root",
})
export class HashService {
  /**
   * Returns SHA-512 Hash of the file as Base64 string format
   * @param buffer file array buffer
   */
  hash(file: File): Observable<string> {
    return this.toBuffer(file).pipe(
      mergeMap((buffer) =>
        from(crypto.subtle.digest("SHA-512", buffer as ArrayBuffer)).pipe(
          mergeMap((hash) => {
            const base64 = this.bytesToBase64(hash);
            console.log(base64);
            return base64;
          })
        )
      )
    );
  }

  toBuffer(file: any): any {
    return from(file.arrayBuffer());
  }

  bytesToBase64(bytes) {
    const base64abc = [
      "A",
      "B",
      "C",
      "D",
      "E",
      "F",
      "G",
      "H",
      "I",
      "J",
      "K",
      "L",
      "M",
      "N",
      "O",
      "P",
      "Q",
      "R",
      "S",
      "T",
      "U",
      "V",
      "W",
      "X",
      "Y",
      "Z",
      "a",
      "b",
      "c",
      "d",
      "e",
      "f",
      "g",
      "h",
      "i",
      "j",
      "k",
      "l",
      "m",
      "n",
      "o",
      "p",
      "q",
      "r",
      "s",
      "t",
      "u",
      "v",
      "w",
      "x",
      "y",
      "z",
      "0",
      "1",
      "2",
      "3",
      "4",
      "5",
      "6",
      "7",
      "8",
      "9",
      "+",
      "/",
    ];

    let result = "",
      i,
      l = bytes.length;
    for (i = 2; i < l; i += 3) {
      result += base64abc[bytes[i - 2] >> 2];
      result += base64abc[((bytes[i - 2] & 0x03) << 4) | (bytes[i - 1] >> 4)];
      result += base64abc[((bytes[i - 1] & 0x0f) << 2) | (bytes[i] >> 6)];
      result += base64abc[bytes[i] & 0x3f];
    }
    if (i === l + 1) {
      // 1 octet yet to write
      result += base64abc[bytes[i - 2] >> 2];
      result += base64abc[(bytes[i - 2] & 0x03) << 4];
      result += "==";
    }
    if (i === l) {
      // 2 octets yet to write
      result += base64abc[bytes[i - 2] >> 2];
      result += base64abc[((bytes[i - 2] & 0x03) << 4) | (bytes[i - 1] >> 4)];
      result += base64abc[(bytes[i - 1] & 0x0f) << 2];
      result += "=";
    }
    return result;
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
