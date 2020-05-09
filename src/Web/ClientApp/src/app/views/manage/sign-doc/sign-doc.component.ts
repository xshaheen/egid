import { Component } from "@angular/core";
import {
  SignDocService,
  SignatureFile,
} from "src/app/services/sign-doc.service";
import { MatDialog } from "@angular/material/dialog";
import { Observable } from "rxjs";
import { CardClient, IsCorrectPin2Query } from "src/app/api";
import { ErrorService } from "src/app/services/error.service";
import { AppModalService } from "src/app/services/modal.service";
import { PasswordDialogComponent } from "src/app/components/password-dialog/password-dialog.component";
import { DomSanitizer } from "@angular/platform-browser";

@Component({
  selector: "eg-sign-doc",
  templateUrl: "./sign-doc.component.html",
  styleUrls: ["./sign-doc.component.scss"],
})
export class SignDocComponent {
  pin2: string = null;
  files: SignatureFile[] = [];

  constructor(
    private signService: SignDocService,
    private dialog: MatDialog,
    private cardClient: CardClient,
    private errorHandler: ErrorService,
    private modal: AppModalService,
    private readonly sanitizer: DomSanitizer
  ) {}

  onAddFile(file: File) {
    if (this.pin2 && this.pin2.length > 4) {
      this.signDoc(file);
      return;
    }

    this.pin2Dialog().subscribe((pin2) => {
      if (!pin2 || pin2.length < 4) {
        this.modal.showNormalSnackBar(
          "يجب ادخال رمز PIN2 حتي تتمكن من توقيع الملفات!"
        );
        return;
      }

      this.cardClient.isCorrectPin2(new IsCorrectPin2Query({ pin2 })).subscribe(
        (correct) => {
          if (correct === true) {
            this.modal.showSuccessSnackBar(
              "رمز PIN2  صحيح جاري توقيع الملفات."
            );
            this.pin2 = pin2;
          } else {
            this.modal.showErrorSnackBar("رمز PIN2 غير صحيح.");
            this.onAddFile(file);
          }
        },
        (error) => this.errorHandler.handleError(error),
        () => this.signDoc(file)
      );
    });
  }

  signDoc(file: File) {
    this.signService.signDoc(file, this.pin2).subscribe(
      (res) => {
        this.files.push(res);
      },
      (err) => this.errorHandler.handleError(err)
    );
  }

  pin2Dialog(): Observable<string> {
    // open a dialog and get a reference to it
    const dialogRef = this.dialog.open(PasswordDialogComponent, {
      width: "450px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
      data: { passwordType: "PIN2" },
    });
    // subscribe to close event to get the result
    return dialogRef.afterClosed();
  }

  sanitize(url: string) {
    return this.sanitizer.bypassSecurityTrustUrl(url);
  }
}
