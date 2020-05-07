import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import { AppTokenService } from "./token.service";

export class SignInModel {
  privateKey!: string;
  pin1!: string;
}

@Injectable({ providedIn: "root" })
export class AuthService {
  constructor(
    private readonly http: HttpClient,
    private readonly router: Router,
    private readonly appTokenService: AppTokenService
  ) {}

  signIn(model: SignInModel): void {
    this.http.post<string>("/api/auth", model).subscribe((token) => {
      if (!token) {
        return;
      }
      this.appTokenService.set(token);
      this.router.navigate(["/main/home"]);
    });
  }

  signOut() {
    this.appTokenService.clear();
    this.router.navigate(["/auth"]);
  }
}
