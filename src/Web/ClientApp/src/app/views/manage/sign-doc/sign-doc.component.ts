import { Component } from "@angular/core";
import { FileModel } from "src/app/components/file/file.component";
import { MatDialog } from "@angular/material/dialog";
import { PasswordDialogComponent } from "src/app/components/password-dialog/password-dialog.component";

@Component({
  selector: "eg-sign-doc",
  templateUrl: "./sign-doc.component.html",
  styleUrls: ["./sign-doc.component.scss"],
})
export class SignDocComponent {
  files: FileModel[] = [];
  password: string;

  constructor(public dialog: MatDialog) {}

  openDialog(): void {
    const dialogRef = this.dialog.open(PasswordDialogComponent, {
      width: "450px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
      data: { passwordType: "PIN2" },
    });

    dialogRef.afterClosed().subscribe((result) => {
      this.password = result;
      console.log(result);
    });
  }

  onAddFile(file: FileModel) {
    this.files.push(file);
    this.uploadToSign(this.files.length - 1);
  }

  // Simulate it for now
  uploadToSign(index: number) {
    const progressInterval = setInterval(() => {
      if (this.files[index].progress === 100) {
        clearInterval(progressInterval);
      } else {
        this.files[index].progress += 5;
      }
    }, 200);
  }

  /**
   * format bytes
   * @param bytes (File size in bytes)
   * @param decimals (Decimals point precision)
   */
  formatBytes(bytes: number, decimals = 2): string {
    if (bytes === 0) {
      return "0 Bytes";
    }
    const k = 1024;
    const dm = decimals <= 0 ? 0 : decimals;
    const sizes = ["Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"];
    const i = Math.floor(Math.log(bytes) / Math.log(k));
    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + " " + sizes[i];
  }
}
