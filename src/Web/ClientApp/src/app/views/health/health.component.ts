import { Component, OnInit } from "@angular/core";
import { BreakpointsService } from "src/app/services/breakpoints.service";

@Component({
  selector: "eg-health",
  template: "<router-outlet></router-outlet>",
})
export class HealthComponent implements OnInit {
  isHandset: boolean;

  constructor(private readonly handset: BreakpointsService) {}

  ngOnInit(): void {
    this.handset.isHandset().subscribe((result) => (this.isHandset = result));
  }
}
