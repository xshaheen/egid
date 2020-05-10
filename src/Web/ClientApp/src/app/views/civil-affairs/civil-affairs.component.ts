import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/services/auth.service";
import { BreakpointsService } from "src/app/services/breakpoints.service";

@Component({
  selector: "eg-civil-affairs",
  templateUrl: "./civil-affairs.component.html",
  styleUrls: ["./civil-affairs.component.scss"],
})
export class CivilAffairsComponent implements OnInit {
  isHandset: boolean;

  constructor(
    public authService: AuthService,
    private breakpoints: BreakpointsService
  ) {}

  ngOnInit(): void {
    this.breakpoints
      .isHandset()
      .subscribe((result) => (this.isHandset = result));
  }
}
