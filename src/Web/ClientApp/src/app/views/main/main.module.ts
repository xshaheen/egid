import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { MainComponent } from "./main.component";
import { Routes, RouterModule } from "@angular/router";

const routes: Routes = [{ path: "", component: MainComponent }];

@NgModule({
  declarations: [MainComponent],
  imports: [CommonModule, RouterModule.forChild(routes)]
})
export class MainModule {}
