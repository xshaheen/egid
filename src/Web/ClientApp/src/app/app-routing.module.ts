import { NgModule } from "@angular/core";
import { Routes, RouterModule } from "@angular/router";
import { AuthGuard } from "./guards/auth.guard";
import { AdminGuard } from "./guards/admin.guard";
import { CivilAffairsGuard } from "./guards/civil-affairs.guard";
import { ForbiddenComponent } from "./views/forbidden/forbidden.component";

const routes: Routes = [
  {
    path: "",
    pathMatch: "full",
    loadChildren: () =>
      import("./views/main/main.module").then((m) => m.MainModule),
  },
  {
    path: "forbidden",
    component: ForbiddenComponent,
  },
  {
    path: "health",
    loadChildren: () =>
      import("./views/health/health.module").then((m) => m.HealthModule),
  },
  {
    path: "admin",
    canActivate: [AuthGuard, AdminGuard],
    loadChildren: () =>
      import("./views/admin/admin.module").then((m) => m.AdminModule),
  },
  {
    canActivate: [AuthGuard, CivilAffairsGuard],
    path: "civil-affairs",
    loadChildren: () =>
      import("./views/civil-affairs/civil-affairs.module").then(
        (m) => m.CivilAffairsModule
      ),
  },
  {
    canActivate: [AuthGuard],
    path: "manage",
    loadChildren: () =>
      import("./views/manage/manage.module").then((m) => m.ManageModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
