import { Injectable } from "@angular/core";
import {
  CanActivate,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";
import { AppTokenService } from "../services/token.service";
import { LoginService } from "../services/login.service";

@Injectable({ providedIn: "root" })
export class AuthGuard implements CanActivate {
  constructor(
    private readonly router: Router,
    private readonly appTokenService: AppTokenService,
    private readonly loginService: LoginService
  ) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.appTokenService.any()) {
      return true;
    }
    const returnUrl = state.url;
    console.log(next.url);
    console.log("RETURN URL", returnUrl);
    this.loginService.login(returnUrl);
    console.log(state.url);
    this.router.navigateByUrl("/");
    return false;
  }
}
