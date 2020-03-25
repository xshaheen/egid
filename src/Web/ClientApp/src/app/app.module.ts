import { BrowserModule } from "@angular/platform-browser";
import { NgModule } from "@angular/core";
import { AppRoutingModule } from "./app-routing.module";

import { AppComponent } from "./app.component";

import { MainModule } from "./views/main/main.module";
import { AdminModule } from "./views/admin/admin.module";
import { CivilAffairsModule } from "./views/civil-affairs/civil-affairs.module";
import { RecordsModule } from "./views/health/records/records.module";
import { AddModule } from "./views/health/add/add.module";
import { BrowserAnimationsModule } from "@angular/platform-browser/animations";

@NgModule({
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    MainModule,
    AdminModule,
    CivilAffairsModule,
    RecordsModule,
    AddModule
  ],
  declarations: [AppComponent],
  bootstrap: [AppComponent]
})
export class AppModule {}
