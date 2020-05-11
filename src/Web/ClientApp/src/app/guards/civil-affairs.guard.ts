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
    const role = this.authService.userRole;
    if (
      this.authService.userRole &&
      (role.toLowerCase() === "civilaffairsempolyee" ||
        role.toLowerCase() === "administrator")
    ) {
      return true;
    }

    this.router.navigateByUrl("forbidden");
    return false;
  }
}
