import { Injectable } from "@angular/core";
import {
  SignatureClient,
  SignHashCommand,
  VerifySignatureResult,
  VerifySignatureCommand,
} from "../api";
import { HashService } from "./hash.service";
import { Observable, of, throwError } from "rxjs";
import { switchMap, catchError } from "rxjs/operators";
import { ErrorService } from "./error.service";

@Injectable({
  providedIn: "root",
})
export class SignDocService {
  constructor(
    private signatureClient: SignatureClient,
    private hashService: HashService,
    private readonly errorService: ErrorService
  ) {}

  signDoc(file: File, pin2: string): Observable<string> {
    return this.hashService.hash(file).pipe(
      switchMap((base64) => {
        return this.signatureClient.sign(
          new SignHashCommand({ base64Sha512DataHash: base64, pin2 })
        );
      }),
      catchError((err: any) => {
        this.errorService.handleError(err);
        return throwError(err);
      })
    );
  }

  // VerifySignature(
  //   signature: File,
  //   file: File
  // ): Observable<VerifySignatureResult> {
  //   let signatureText: string;

  //   const reader = new FileReader();
  //   // Todo: asset that reader.result is not ArrayBuffer by validate file type or throw error.
  //   reader.onload = () => (signatureText = reader.result as string);
  //   reader.readAsText(signature, "utf-8");

  //   return this.hashService.toBuffer(file).pipe(
  //     mergeMap((buffer) =>
  //       this.hashService.hash(buffer).pipe(
  //         mergeMap((hash) =>
  //           this.signatureClient.verify(
  //             new VerifySignatureCommand({
  //               base64Sha512DataHash: hash,
  //               signature: signatureText,
  //             })
  //           )
  //         )
  //       )
  //     )
  //   );
  // }
}
