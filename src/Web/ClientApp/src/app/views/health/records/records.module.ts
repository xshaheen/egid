import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { RecordsComponent } from "./records.component";
import { Routes, RouterModule } from "@angular/router";

const routes: Routes = [{ path: "", component: RecordsComponent }];

@NgModule({
  declarations: [RecordsComponent],
  imports: [CommonModule, RouterModule.forChild(routes)]
})
export class RecordsModule {}
