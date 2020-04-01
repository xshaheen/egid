import { Component, OnInit } from "@angular/core";

import { Observable } from "rxjs";
import { map, shareReplay } from "rxjs/operators";
import { Breakpoints, BreakpointObserver } from "@angular/cdk/layout";

@Component({
  selector: "eg-manage",
  templateUrl: "./manage.component.html",
  styleUrls: ["./manage.component.scss"]
})
export class ManageComponent {
  constructor(private breakpointObserver: BreakpointObserver) {}

  isHandset$: Observable<boolean> = this.breakpointObserver
    .observe(Breakpoints.Handset)
    .pipe(
      map(result => result.matches),
      shareReplay()
    );
}
