import { Component } from "@angular/core";
import { MatDialog } from "@angular/material/dialog";
import {
  CreateCitizenCommand,
  CreateCardCommand,
  CardClient,
  CitizensClient,
} from "src/app/api";
import {
  WriteQrDialogComponent,
  WriteQrCommand,
} from "src/app/components/write-qr-dialog/write-qr-dialog-dialog.component";
import { NgForm } from "@angular/forms";
import { AppModalService } from "src/app/services/modal.service";

// TODO: make to separate step and separated requests

class Create {
  citizen = new CreateCitizenCommand();
  card = new CreateCardCommand();
}

@Component({
  templateUrl: "./register-citizen.component.html",
  styleUrls: ["./register-citizen.component.scss"],
})
export class RegisterCitizenComponent {
  command = new Create();
  photo: File;

  constructor(
    private readonly dialog: MatDialog,
    private readonly cardClient: CardClient,
    private readonly citizenClient: CitizensClient,
    private readonly modal: AppModalService
  ) {}

  submit(_: NgForm) {
    this.citizenClient.post(this.command.citizen).subscribe(
      (result) => {
        const dialog = this.openWriteQrDialog(
          "كود السجل المرضي",
          result.healthInfoId
        );
        this.command.card.ownerId = result.citizenId;

        this.cardClient.post(this.command.card).subscribe(
          (res) => {
            dialog.afterClosed().subscribe(() => {
              this.openWriteQrDialog("كود البطاقة", res);
            });
          },
          (err) => {
            let errors;
            console.log(err);
            try {
              errors = JSON.parse(err.response).errors;
            } catch {
              this.modal.showErrorSnackBar("حدث خطأ اثناء تنفيذ الطلب");
              return;
            }

            if (errors && errors.Pin1) {
              this.modal.showErrorSnackBar(errors.Pin1[0]);
            } else if (errors && errors.Pin2) {
              this.modal.showErrorSnackBar(errors.Pin2[0]);
            } else if (errors && errors.Puk) {
              this.modal.showErrorSnackBar(errors.Puk[0]);
            } else if (errors && errors.CardId) {
              this.modal.showErrorSnackBar(errors.CardId[0]);
            }
          }
        );
      },
      (err) => {
        let errors;
        console.log(err);
        try {
          errors = JSON.parse(err.response).errors;
        } catch {
          this.modal.showErrorSnackBar("حدث خطأ اثناء تنفيذ الطلب");
          return;
        }

        if (errors && errors.FullName) {
          this.modal.showErrorSnackBar(errors.FullName[0]);
        } else if (errors && errors.FirstName) {
          this.modal.showErrorSnackBar(errors.Pin2[0]);
        } else if (errors && errors.DateOfBirth) {
          this.modal.showErrorSnackBar(errors.DateOfBirth[0]);
        }
      }
    );
  }

  file(f: File) {
    this.photo = f;
  }

  private openWriteQrDialog(dialogTitle: string, str: string) {
    return this.dialog.open(WriteQrDialogComponent, {
      width: "450px",
      direction: "rtl",
      disableClose: true,
      closeOnNavigation: true,
      data: new WriteQrCommand(str, dialogTitle),
    });
  }
}
