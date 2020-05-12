import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Routes, RouterModule } from "@angular/router";
import { MaterialModule } from "src/app/material.module";
import { AuthGuard } from "src/app/guards/auth.guard";

import { HealthComponent } from "./health.component";
import { AddComponent } from "./add/add.component";
import { RecordsComponent } from "./records/records.component";
import { MainComponent } from "./main/main.component";

const route: Routes = [
  {
    path: "",
    component: HealthComponent,
    children: [
      { path: "m", component: MainComponent, canActivate: [AuthGuard] },
      { path: "add", component: AddComponent, canActivate: [AuthGuard] },
      { path: "records", component: RecordsComponent },
    ],
  },
  { path: "**", redirectTo: "" },
];

@NgModule({
  declarations: [HealthComponent, AddComponent, RecordsComponent],
  imports: [CommonModule, RouterModule.forChild(route), MaterialModule],
})
export class HealthModule {}
