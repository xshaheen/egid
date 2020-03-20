import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { AddComponent } from "./add.component";
import { Routes, RouterModule } from "@angular/router";

const routes: Routes = [{ path: "", component: AddComponent }];

@NgModule({
  declarations: [AddComponent],
  imports: [CommonModule, RouterModule.forChild(routes)]
})
export class AddModule {}
