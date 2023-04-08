import { Injectable } from "@angular/core";
import { async } from "@angular/core/testing";
import {
    ActivatedRouteSnapshot,
    CanActivate,
    Router,
    RouterStateSnapshot,
    UrlTree,
} from "@angular/router";
import { Observable } from "rxjs";
import { AuthService } from "../Services/auth.service";

@Injectable({
    providedIn: "root",
})
export class AuthGuard implements CanActivate {
    constructor(private authService: AuthService, private router: Router) {}
    canActivate(
        route: ActivatedRouteSnapshot,
        state: RouterStateSnapshot
    ):
        | Observable<boolean | UrlTree>
        | Promise<boolean | UrlTree>
        | boolean
        | UrlTree {
        const returnUrl = route.url.join("/");
        if (this.authService.LoggedIn()) {
            return true;
        }
        this.router.navigate(["/auth/login"], {
            queryParams: {
                returnUrl: returnUrl,
            },
            queryParamsHandling: "merge",
        });
        return false;
    }
}
