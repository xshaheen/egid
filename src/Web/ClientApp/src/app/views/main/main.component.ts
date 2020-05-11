import { Component, OnInit } from "@angular/core";
import { AuthService } from "src/app/services/auth.service";
import { LoginService } from "src/app/services/login.service";
import { BreakpointsService } from "src/app/services/breakpoints.service";

@Component({
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"],
})
export class MainComponent implements OnInit {
  isHandset: boolean;

  constructor(
    private breakpoints: BreakpointsService,
    public authService: AuthService,
    public loginService: LoginService
  ) {}

  ngOnInit(): void {
    this.breakpoints
      .isHandset()
      .subscribe((result) => (this.isHandset = result));
  }
}
