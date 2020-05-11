import { Component, OnInit } from "@angular/core";
import {
  UpdateEmergencyPhonesCommand,
  IHealthInfoVm,
  HealthInfoClient,
  IEmergencyInfo,
} from "src/app/api";
import { AuthService } from "src/app/services/auth.service";
import { ErrorService } from "src/app/services/error.service";
import { NgForm } from "@angular/forms";
import { AppModalService } from "src/app/services/modal.service";

@Component({
  templateUrl: "./update-health-info.component.html",
  styleUrls: ["./update-health-info.component.scss"],
})
export class UpdateHealthInfoComponent implements OnInit {
  command = new UpdateEmergencyPhonesCommand();

  constructor(
    private client: HealthInfoClient,
    private authService: AuthService,
    private errorHandler: ErrorService,
    private modal: AppModalService
  ) {}

  ngOnInit(): void {
    const healthId = this.authService.healthId;

    this.client.emergencyInfo(healthId).subscribe(
      (result) => {
        this.command.healthInfoId = healthId;
        this.command.phone1 = result.phone1;
        this.command.phone2 = result.phone2;
        this.command.phone3 = result.phone3;
        this.command.notes = result.notes;
      },
      (err) => this.errorHandler.handleError(err)
    );
  }

  Submit(form: NgForm) {
    this.client.emergencyPhones(this.command).subscribe(
      () => {
        this.modal.showSuccessSnackBar("✔ تم حفظ التغيرات.");
      },
      (err) => {
        const errors = JSON.parse(err.response).errors;

        if (errors && errors.Phone1) {
          this.modal.showErrorSnackBar("الرقم الاول: " + errors.Phone1[0]);
        } else if (errors && errors.Phone2) {
          this.modal.showErrorSnackBar("الرقم الثاني: " + errors.Phone1[0]);
        } else if (errors && errors.Phone3) {
          this.modal.showErrorSnackBar("الرقم الثالث: " + errors.Phone1[0]);
        } else if (errors && errors.Note) {
          this.modal.showErrorSnackBar("الملاحظات: " + errors.Phone1[0]);
        }
      }
    );
  }
}
