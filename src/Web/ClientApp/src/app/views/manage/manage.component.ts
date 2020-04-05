import { Component, OnInit } from "@angular/core";

import { Observable } from "rxjs";
import { map, shareReplay } from "rxjs/operators";
import { Breakpoints, BreakpointObserver } from "@angular/cdk/layout";
import { BreakpointsService } from "src/app/services/breakpoints.service";

@Component({
  selector: "eg-manage",
  templateUrl: "./manage.component.html",
  styleUrls: ["./manage.component.scss"],
})
export class ManageComponent implements OnInit {
  isHandset: boolean;

  constructor(private breakpoints: BreakpointsService) {}

  ngOnInit(): void {
    this.breakpoints
      .isHandset()
      .subscribe((result) => (this.isHandset = result));
  }
}
