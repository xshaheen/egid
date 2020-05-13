import { Component, OnInit } from "@angular/core";

import {
  ChangePin2Command,
  ChangePin1Command,
  ChangePukCommand,
  CardClient,
} from "src/app/api";

import { NgForm } from "@angular/forms";
import { AppModalService } from "src/app/services/modal.service";
import { AuthService } from "src/app/services/auth.service";

@Component({
  templateUrl: "./card.component.html",
  styleUrls: ["./card.component.scss"],
})
export class CardComponent implements OnInit {
  changePin2Command: ChangePin2Command;
  changePin1Command: ChangePin1Command;
  changePukCommand: ChangePukCommand;

  constructor(
    private client: CardClient,
    private modal: AppModalService,
    private authenticator: AuthService
  ) {
    this.changePin2Command = new ChangePin2Command();
    this.changePin1Command = new ChangePin1Command();
    this.changePukCommand = new ChangePukCommand();
  }

  ngOnInit(): void {
    this.changePin1Command.cardId = this.authenticator.cardId;
    this.changePin2Command.cardId = this.authenticator.cardId;
    this.changePukCommand.cardId = this.authenticator.cardId;
  }

  submitPin1(_: NgForm) {
    this.client.pin1(this.changePin1Command).subscribe(
      () => {
        this.modal.showSuccessSnackBar("تم تغير PIN1 بنجاح ✔");
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

        if (errors && errors.NewPin1) {
          this.modal.showErrorSnackBar(errors.NewPin1[0]);
        } else if (errors && errors.Puk) {
          this.modal.showErrorSnackBar(errors.Puk[0]);
        } else if (errors && errors.CardId) {
          this.modal.showErrorSnackBar(errors.CardId[0]);
        }
      }
    );
  }

  submitPin2(_: NgForm) {
    this.client.pin2(this.changePin2Command).subscribe(
      () => {
        this.modal.showSuccessSnackBar("تم تغير PIN1 بنجاح ✔");
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

        if (errors && errors.NewPin2) {
          this.modal.showErrorSnackBar(errors.NewPin2[0]);
        } else if (errors && errors.Puk) {
          this.modal.showErrorSnackBar(errors.Puk[0]);
        } else if (errors && errors.CardId) {
          this.modal.showErrorSnackBar(errors.CardId[0]);
        }
      }
    );
  }
  submitPuk(_: NgForm) {
    this.client.puk(this.changePukCommand).subscribe(
      () => {
        this.modal.showSuccessSnackBar("تم تغير PIN1 بنجاح ✔");
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

        if (errors && errors.NewPuk) {
          this.modal.showErrorSnackBar(errors.NewPuk[0]);
        } else if (errors && errors.CurrentPuk) {
          this.modal.showErrorSnackBar(errors.CurrentPuk[0]);
        } else if (errors && errors.CardId) {
          this.modal.showErrorSnackBar(errors.CardId[0]);
        }
      }
    );
  }
}
