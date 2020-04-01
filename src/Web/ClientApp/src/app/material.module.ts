import { NgModule } from "@angular/core";

import { MatSidenavModule } from "@angular/material/sidenav";
import { MatToolbarModule } from "@angular/material/toolbar";
import { MatListModule } from "@angular/material/list";
import { MatSliderModule } from "@angular/material/slider";
import { MatSlideToggleModule } from "@angular/material/slide-toggle";
import { MatButtonModule } from "@angular/material/button";
import { MatButtonToggleModule } from "@angular/material/button-toggle";
import { MatExpansionModule } from "@angular/material/expansion";
import { MatFormFieldModule } from "@angular/material/form-field";
import { MatGridListModule } from "@angular/material/grid-list";
import { LayoutModule } from "@angular/cdk/layout";
import { MatDividerModule } from "@angular/material/divider";
import { MatSelectModule } from "@angular/material/select";
import { MatTreeModule } from "@angular/material/tree";

@NgModule({
  imports: [
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatGridListModule,
    LayoutModule,
    MatDividerModule,
    MatSelectModule,
    MatTreeModule
  ],
  exports: [
    MatSidenavModule,
    MatToolbarModule,
    MatListModule,
    MatSliderModule,
    MatSlideToggleModule,
    MatButtonModule,
    MatButtonToggleModule,
    MatExpansionModule,
    MatFormFieldModule,
    MatGridListModule,
    LayoutModule,
    MatDividerModule,
    MatSelectModule,
    MatTreeModule
  ]
})
export class MaterialModule {}
