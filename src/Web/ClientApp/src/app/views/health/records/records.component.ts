import { Component, OnInit } from "@angular/core";
import { BreakpointsService } from "src/app/services/breakpoints.service";

@Component({
  selector: "eg-records",
  templateUrl: "./records.component.html",
  styleUrls: ["./records.component.scss"],
})
export class RecordsComponent implements OnInit {
  isHandset: boolean;

  constructor(private handset: BreakpointsService) {}

  ngOnInit(): void {
    this.handset.isHandset().subscribe((result) => (this.isHandset = result));
  }
}
