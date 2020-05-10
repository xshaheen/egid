import { Injectable } from "@angular/core";
import {
  CanActivate,
  Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
} from "@angular/router";

import { AuthService } from "../services/auth.service";

@Injectable({ providedIn: "root" })
export class CivilAffairsGuard implements CanActivate {
  constructor(
    private readonly router: Router,
    private readonly authService: AuthService
  ) {}

  canActivate(next: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    if (this.authService.userRole.toLowerCase() === "administrator") {
      return true;
    }

    this.router.navigateByUrl("/forbidden");
    return false;
  }
}
