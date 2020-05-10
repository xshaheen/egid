import { Component } from "@angular/core";
import { AuthService } from "src/app/services/auth.service";
import { LoginService } from "src/app/services/login.service";

@Component({
  templateUrl: "./main.component.html",
  styleUrls: ["./main.component.scss"],
})
export class MainComponent {
  constructor(
    public authService: AuthService,
    public loginService: LoginService
  ) {}
}
