
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { AccountService } from 'src/app/swagger/shared/services';
import { JwtAuthentificationResponse, LoginRequest } from 'src/app/swagger/shared/models';

@Injectable({
    providedIn: 'root'
})

export class AuthenticationHelper {
    private currentUserSubject: BehaviorSubject<JwtAuthentificationResponse>;
    public currentUser: Observable<JwtAuthentificationResponse>;

    constructor(private authService: AccountService, private router: Router, private toastr: ToastrService) {
        this.currentUserSubject = new BehaviorSubject<JwtAuthentificationResponse>(JSON.parse(localStorage.getItem('currentUser')));
        this.currentUser = this.currentUserSubject.asObservable();
    }

    public get currentUserValue(): JwtAuthentificationResponse {
        return this.currentUserSubject.value;
    }

    public set setCurrentUser(user: JwtAuthentificationResponse) {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
    }

    getRefreshToken(): string {
        if (this.currentUserValue) {
            return this.currentUserValue.refreshToken;
        }
        return undefined;
    }

    login(request: LoginRequest) {
        this.authService.AccountLogin(request).subscribe(user => {
            if (user && user.accessToken) {
                this.setCurrentUser = user;
            }
            this.router.navigate(['']);
        });
    }

    logout() {
        localStorage.removeItem('currentUser');
        this.currentUserSubject.next(null);
        this.router.navigate(['/auth']);
    }


}
