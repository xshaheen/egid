import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { ManageComponent } from "./manage.component";
import { SignDocComponent } from "./sign-doc/sign-doc.component";
import { VerifySignatureComponent } from "./verify-signature/verify-signature.component";
import { UpdateInfoComponent } from "./update-info/update-info.component";
import { ChangePinComponent } from "./change-pin/change-pin.component";
import { UpdateHealthInfoComponent } from "./update-health-info/update-health-info.component";

const routes: Routes = [
  {
    path: "",
    component: ManageComponent,
    children: [
      { path: "", redirectTo: "sign" },
      { path: "sign", component: SignDocComponent },
      { path: "verify", component: VerifySignatureComponent },
      { path: "update", component: UpdateInfoComponent },
      { path: "change-pin", component: ChangePinComponent },
      { path: "health-info", component: UpdateHealthInfoComponent }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ManageRoutingModule {}
