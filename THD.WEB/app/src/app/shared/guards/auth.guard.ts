import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthenticationHelper } from '../helpers/auth.helper';
@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

    constructor(
        private router: Router,
        private authenticationHelper: AuthenticationHelper
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
        const currentUser = this.authenticationHelper.currentUserValue;
        if (currentUser) {
            return true;
        }
        this.router.navigate(['/auth']);
        return false;
    }
}


