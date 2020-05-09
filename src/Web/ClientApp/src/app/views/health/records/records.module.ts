import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { Routes, RouterModule } from "@angular/router";
import { MaterialModule } from "src/app/material.module";

import { RecordsComponent } from "./records.component";

const routes: Routes = [{ path: "", component: RecordsComponent }];

@NgModule({
  declarations: [RecordsComponent],
  imports: [CommonModule, MaterialModule, RouterModule.forChild(routes)],
})
export class RecordsModule {}
