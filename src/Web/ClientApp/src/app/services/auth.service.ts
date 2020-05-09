import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AppTokenService } from "./token.service";
import { AuthClient, LoginCommand } from "../api";
import { ErrorService } from "./error.service";
import { AppModalService } from "./modal.service";

@Injectable({ providedIn: "root" })
export class AuthService {
  constructor(
    private readonly authClient: AuthClient,
    private readonly router: Router,
    private readonly appTokenService: AppTokenService,
    private readonly errorService: ErrorService,
    private readonly modal: AppModalService
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
        this.modal.showSuccessSnackBar("تم تسجيل الدخول بنجاح.");
      },
      (err) => this.errorService.handleError(err)
    );
  }

  signOut() {
    this.appTokenService.clear();
    this.router.navigateByUrl("/");
    this.modal.showSuccessSnackBar("تم تسجيل الخروج بنجاح.");
  }
}
