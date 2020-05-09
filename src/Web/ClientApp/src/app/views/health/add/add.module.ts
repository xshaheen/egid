import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MaterialModule } from "src/app/material.module";
import { Routes, RouterModule } from "@angular/router";

import { AddComponent } from "./add.component";

const routes: Routes = [{ path: "", component: AddComponent }];

@NgModule({
  declarations: [AddComponent],
  imports: [CommonModule, MaterialModule, RouterModule.forChild(routes)],
})
export class AddModule {}
