import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AppTokenService } from "./token.service";
import { AuthClient, LoginCommand } from "../api";
import { AppModalService } from "./modal.service";
import { map } from "rxjs/operators";
import { Observable } from "rxjs";

@Injectable({ providedIn: "root" })
export class AuthService {
  constructor(
    private readonly authClient: AuthClient,
    private readonly router: Router,
    private readonly appTokenService: AppTokenService,
    private readonly modal: AppModalService
  ) {}

  public get isAuthenticated(): boolean {
    return this.appTokenService.any();
  }

  public get userRole(): string {
    return this.appTokenService.decode("role");
  }

  public get userName(): string {
    return this.appTokenService.decode("firstname");
  }

  public get userImg(): string {
    return this.appTokenService.decode("img");
  }

  signIn(model: LoginCommand): Observable<void> {
    return this.authClient.signIn(model).pipe(
      map((token) => {
        if (!token) {
          return;
        }
        this.appTokenService.set(token);
      })
    );
  }

  signOut() {
    this.appTokenService.clear();
    this.router.navigateByUrl("/");
    this.modal.showSuccessSnackBar("تم تسجيل الخروج بنجاح.");
  }
}
