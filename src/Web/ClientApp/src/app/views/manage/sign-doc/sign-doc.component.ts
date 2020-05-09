import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { SignDocService } from "src/app/services/sign-doc.service";
import { MatDialog } from "@angular/material/dialog";
import { PasswordDialogComponent } from "src/app/components/password-dialog/password-dialog.component";
import { Observable, from } from "rxjs";

@Component({
  selector: "eg-sign-doc",
  templateUrl: "./sign-doc.component.html",
  styleUrls: ["./sign-doc.component.scss"],
})
export class SignDocComponent {
  files: File[] = [];
  link: string;

  constructor(
    private signService: SignDocService,
    private dialog: MatDialog,
    private readonly router: Router
  ) {}

  openPin2Dialog(): Observable<string> {
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

  // openConfirmDialog(): void {
  //   // open a dialog and get a reference to it
  //   const dialogRef = this.dialog.open(ConfirmDialogComponent, {
  //     width: "450px",
  //     direction: "rtl",
  //     disableClose: true,
  //     closeOnNavigation: true,
  //     data: "هل انت متاكد من انك تريد حذف هذا الموظف؟",
  //   });
  //   // subscribe to close event to get the result
  //   dialogRef.afterClosed().subscribe((result) => {
  //     if (result) {
  //       this.confirm = result;
  //     } else {
  //       this.confirm = false;
  //     }
  //   });
  // }

  onAddFile(file: File) {
    this.openPin2Dialog().subscribe((pin2) => {
      this.signService.signDoc(file, pin2).subscribe((res) => {
        this.link =
          "data:application/octet-stream;charset=utf-8;base64,//" + res;
        this.files.push(file);
        console.log(res);
      });
    });
  }
}

// Simulate it for now
// uploadToSign(index: number) {
//   const progressInterval = setInterval(() => {
//     if (this.files[index].progress === 100) {
//       clearInterval(progressInterval);
//     } else {
//       this.files[index].progress += 5;
//     }
//   }, 200);
// }

// /**
//  * format bytes
//  * @param bytes (File size in bytes)
//  * @param decimals (Decimals point precision)
//  */
// formatBytes(bytes: number, decimals = 2): string {
//   if (bytes === 0) {
//     return "0 Bytes";
//   }
//   const k = 1024;
//   const dm = decimals <= 0 ? 0 : decimals;
//   const sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];
//   const i = Math.floor(Math.log(bytes) / Math.log(k));
//   return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + " " + sizes[i];
// }
