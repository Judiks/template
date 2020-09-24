import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot } from '@angular/router';
import { AuthenticationHelper } from '../helpers/auth.helper';

@Injectable({ providedIn: 'root' })
export class AdminGuard implements CanActivate {

    constructor(
        private router: Router,
        private authenticationHelper: AuthenticationHelper
    ) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        const currentUser = this.authenticationHelper.currentUserValue;
        if (currentUser.roles.includes('SuperAdmin')  || currentUser.roles.includes('SupperAdmin') || currentUser.roles.includes('Admin')) {
            return true;
        }
        this.router.navigate(['/auth']);
        return false;
    }
}


