import { Component, OnInit } from "@angular/core";
import { SignDocService } from "src/app/services/sign-doc.service";
import { VerifySignatureResult } from "src/app/api";
import { ErrorService } from "src/app/services/error.service";

interface Result {
  valid: boolean;
  fullName: string;
  img: string;
}

@Component({
  templateUrl: "./verify-signature.component.html",
  styleUrls: ["./verify-signature.component.scss"],
})
export class VerifySignatureComponent {
  signatureFile: File = null;
  docFile: File = null;
  result: Result;
  done = false;

  constructor(
    private readonly signService: SignDocService,
    private readonly errorHandler: ErrorService
  ) {}

  public get isValid(): boolean {
    return this.signatureFile != null && this.docFile != null;
  }

  addDocFile(files: FileList | null) {
    if (!files) {
      return;
    }
    this.docFile = files.item(0) as File;
  }

  addSignFile(files: FileList | null) {
    if (!files) {
      return;
    }
    this.signatureFile = files.item(0) as File;
  }

  verify() {
    this.signService
      .VerifySignature(this.signatureFile, this.docFile)
      .subscribe(
        (result) => {
          const r: Result = {
            fullName:
              result.fullName.firstName +
              " " +
              result.fullName.secondName +
              " " +
              result.fullName.thirdName +
              " " +
              result.fullName.lastName,
            img: result.photo,
            valid: result.valid,
          };

          this.result = r;
        },
        (err) => this.errorHandler.handleError(err)
      );
  }
}
