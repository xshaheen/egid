import { Injectable } from "@angular/core";
import {
  SignatureClient,
  SignHashCommand,
  VerifySignatureResult,
  VerifySignatureCommand,
} from "../api";
import { HashService } from "./hash.service";
import { Observable, throwError } from "rxjs";
import { switchMap, catchError, map, mergeMap, buffer } from "rxjs/operators";
import { ErrorService } from "./error.service";

export interface SignatureFile {
  fileUrl: string;
  fileName: string;
}

@Injectable({
  providedIn: "root",
})
export class SignDocService {
  constructor(
    private readonly signatureClient: SignatureClient,
    private readonly hashService: HashService,
    private readonly errorService: ErrorService
  ) {}

  signDoc(file: File, pin2: string): Observable<SignatureFile> {
    return this.hashService.hash(file).pipe(
      switchMap((base64) => {
        return this.signatureClient.sign(
          new SignHashCommand({
            base64Sha512DataHash: base64,
            pin2,
            fileName: file.name,
          })
        );
      }),
      map((response) => {
        const f: SignatureFile = {
          fileName: response.fileName,
          fileUrl: URL.createObjectURL(response.data),
        };
        return f;
      }),
      catchError((err: any) => {
        this.errorService.handleError(err);
        return throwError(err);
      })
    );
  }

  VerifySignature(
    signature: File,
    file: File
  ): Observable<VerifySignatureResult> {
    let signatureText: string;

    const reader = new FileReader();
    reader.onload = () => (signatureText = reader.result as string);
    reader.readAsText(signature, "utf-8");

    return this.hashService.hash(file).pipe(
      mergeMap((hash) =>
        this.signatureClient.verify(
          new VerifySignatureCommand({
            base64Sha512DataHash: hash,
            signature: signatureText,
          })
        )
      )
    );
  }
}
