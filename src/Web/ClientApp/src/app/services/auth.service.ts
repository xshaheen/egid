import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AppTokenService } from "./token.service";
import { AuthClient, LoginCommand } from "../api";

@Injectable({ providedIn: "root" })
export class AuthService {
  constructor(
    private readonly authClient: AuthClient,
    private readonly router: Router,
    private readonly appTokenService: AppTokenService
  ) {}

  public get isAuthenticated(): boolean {
    return this.appTokenService.any();
  }

  signIn(model: LoginCommand): void {
    this.authClient.signIn(model).subscribe((token) => {
      if (!token) {
        return;
      }
      this.appTokenService.set(token);
    });
  }

  signOut() {
    this.appTokenService.clear();
    this.router.navigate(["/main/home"]);
  }
}
