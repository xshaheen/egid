import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AppTokenService } from "./token.service";
import { AuthClient, LoginCommand } from "../api";
import { ErrorService } from "./error.service";

@Injectable({ providedIn: "root" })
export class AuthService {
  constructor(
    private readonly authClient: AuthClient,
    private readonly router: Router,
    private readonly appTokenService: AppTokenService,
    private readonly errorService: ErrorService
  ) {}

  public get isAuthenticated(): boolean {
    return this.appTokenService.any();
  }

  signIn(model: LoginCommand): void {
    this.authClient.signIn(model).subscribe(
      (token) => {
        if (!token) {
          return;
        }
        this.appTokenService.set(token);
      },
      (err) => this.errorService.handleError(err)
    );
  }

  signOut() {
    this.appTokenService.clear();
    this.router.navigate(["/main/home"]);
  }
}
