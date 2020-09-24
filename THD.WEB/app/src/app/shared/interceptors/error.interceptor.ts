import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, BehaviorSubject, EMPTY } from 'rxjs';
import { catchError, filter, take, switchMap, finalize } from 'rxjs/operators';
import { AuthenticationHelper } from '../helpers/auth.helper';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AccountService } from 'src/app/swagger/shared/services';
import { JwtAuthentificationResponse, RefreshTokenRequest } from 'src/app/swagger/shared/models';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    private isRefreshing = false;
    private refreshTokenSubject: BehaviorSubject<any> = new BehaviorSubject<any>(null);

    constructor(private authenticationHelper: AuthenticationHelper, private accountService: AccountService,
                private toastr: ToastrService, private spinner: NgxSpinnerService) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(request).pipe(catchError(err => {
            this.spinner.hide();
            const isTokenExpired = err.headers.has('token-expired');
            const isRefreshTokenInvalid = err.headers.has('invalid-refresh-token');
            if (err instanceof HttpErrorResponse && err.status === 401 && isTokenExpired && !isRefreshTokenInvalid) {
                return this.handle401withRefresh(request, next);
            }

            if (err instanceof HttpErrorResponse && err.status === 401 && (!isTokenExpired || isRefreshTokenInvalid)) {
                this.authenticationHelper.logout();
            }
            if (err instanceof HttpErrorResponse && err.status === 403) {
                this.toastr.error('You have not permission for this page');
            }
            if (err instanceof HttpErrorResponse && err.status === 400) {
                this.toastr.error('Internal server error');
            }
            if (err instanceof HttpErrorResponse && err.status === 500) {
                this.toastr.error(err.error, 'Error');
            }
            return throwError(err);
        }));
    }

    private handle401withRefresh(request: HttpRequest<any>, next: HttpHandler) {
        if (!this.isRefreshing) {
            this.isRefreshing = true;
            this.refreshTokenSubject.next(null);
            return this.refresh().pipe(
                switchMap((user: JwtAuthentificationResponse) => {
                    console.clear();
                    if (user) {
                        this.authenticationHelper.setCurrentUser = user;
                        this.refreshTokenSubject.next(user.refreshToken);
                        return next.handle(this.addToken(request, user.accessToken));
                    }
                    this.authenticationHelper.logout();
                }),
                finalize(() => {
                    this.isRefreshing = false;
                }));
        }

        return this.refreshTokenSubject.pipe(
            filter(token => token != null),
            take(1),
            switchMap(jwt => {
                return next.handle(this.addToken(request, jwt));
            }), catchError(err => {
                return throwError(err);
            }));
    }


    private refresh(): Observable<JwtAuthentificationResponse> {
        const refreshToken = this.authenticationHelper.getRefreshToken();
        if (!refreshToken) {
            this.authenticationHelper.logout();
            return EMPTY;
        }
        const request: RefreshTokenRequest = {
            refreshToken: refreshToken
        }
        return this.accountService.AccountRefreshToken(request);
    }

    private addToken(request: HttpRequest<any>, token: string) {
        return request.clone({
            setHeaders: {
                Authorization: `Bearer ${token}`
            }
        });
    }
}
