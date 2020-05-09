import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MaterialModule } from "src/app/material.module";
import { Routes, RouterModule } from "@angular/router";

import { MainComponent } from "./main.component";

const routes: Routes = [{ path: "", component: MainComponent }];

@NgModule({
  declarations: [MainComponent],
  imports: [CommonModule, MaterialModule, RouterModule.forChild(routes)],
})
export class MainModule {}
