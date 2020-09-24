import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthenticationHelper } from '../helpers/auth.helper';
import { environment } from 'src/environments/environment';
import { NgxSpinnerService } from 'ngx-spinner';
import { tap, catchError } from 'rxjs/operators';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private authenticationHelper: AuthenticationHelper, private spinner: NgxSpinnerService) { }
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (request instanceof HttpRequest) {
            this.spinner.show();
        }
        if (request.url.includes('/api/PriceCalculation/Calculation')) {
            this.spinner.hide();
        }
        const currentUser = this.authenticationHelper.currentUserValue;
        const isLoggedIn = currentUser && currentUser.accessToken;
        const isApiUrl = request.url.startsWith(environment.apiURL);
        if (isLoggedIn && isApiUrl) {
            request = request.clone({
                setHeaders: {
                    Authorization: `Bearer ${currentUser.accessToken}`
                }
            });
        }
        return next.handle(request).pipe(
            tap(event => {
                if (event instanceof HttpResponse) {
                    this.spinner.hide();
                }
            })
        );
    }
}
