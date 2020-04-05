import { Component, OnInit } from "@angular/core";
import { BreakpointsService } from "src/app/services/breakpoints.service";

@Component({
  templateUrl: "./admin.component.html",
  styleUrls: ["./admin.component.scss"],
})
export class AdminComponent implements OnInit {
  isHandset: boolean;

  constructor(private handset: BreakpointsService) {}

  ngOnInit(): void {
    this.handset.isHandset().subscribe((result) => (this.isHandset = result));
  }
}
