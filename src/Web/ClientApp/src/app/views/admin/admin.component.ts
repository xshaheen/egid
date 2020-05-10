import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/services/auth.service";
import { BreakpointsService } from "src/app/services/breakpoints.service";

@Component({
  templateUrl: "./admin.component.html",
  styleUrls: ["./admin.component.scss"],
})
export class AdminComponent implements OnInit {
  isHandset: boolean;

  constructor(
    public authService: AuthService,
    private handset: BreakpointsService
  ) {}

  ngOnInit(): void {
    this.handset.isHandset().subscribe((result) => (this.isHandset = result));
  }
}
