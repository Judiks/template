/* tslint:disable */
import { Injectable } from '@angular/core';
import { HttpClient, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { BaseService as __BaseService } from '../base-service';
import { ApiConfiguration as __Configuration } from '../api-configuration';
import { StrictHttpResponse as __StrictHttpResponse } from '../strict-http-response';
import { Observable as __Observable } from 'rxjs';
import { map as __map, filter as __filter } from 'rxjs/operators';

import { JwtAuthentificationResponse } from '../models/jwt-authentification-response';
import { LoginRequest } from '../models/login-request';
import { RefreshTokenRequest } from '../models/refresh-token-request';
import { UserResponse } from '../models/user-response';
import { CreateUserRequest } from '../models/create-user-request';
import { GetAllUserResponse } from '../models/get-all-user-response';
import { UpdateUserRequest } from '../models/update-user-request';
@Injectable({
  providedIn: 'root',
})
class AccountService extends __BaseService {
  static readonly AccountLoginPath = '/api/Account/Login';
  static readonly AccountRefreshTokenPath = '/api/Account/RefreshToken';
  static readonly AccountCreatePath = '/api/Account/Create';
  static readonly AccountGetAllPath = '/api/Account/GetAll';
  static readonly AccountGetByIdPath = '/api/Account/GetById';
  static readonly AccountUpdatePath = '/api/Account/Update';
  static readonly AccountDeletePath = '/api/Account/Delete';

  constructor(
    config: __Configuration,
    http: HttpClient
  ) {
    super(config, http);
  }

  /**
   * @param request undefined
   */
  AccountLoginResponse(request: LoginRequest): __Observable<__StrictHttpResponse<JwtAuthentificationResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = request;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Account/Login`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<JwtAuthentificationResponse>;
      })
    );
  }
  /**
   * @param request undefined
   */
  AccountLogin(request: LoginRequest): __Observable<JwtAuthentificationResponse> {
    return this.AccountLoginResponse(request).pipe(
      __map(_r => _r.body as JwtAuthentificationResponse)
    );
  }

  /**
   * @param request undefined
   */
  AccountRefreshTokenResponse(request: RefreshTokenRequest): __Observable<__StrictHttpResponse<JwtAuthentificationResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = request;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Account/RefreshToken`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<JwtAuthentificationResponse>;
      })
    );
  }
  /**
   * @param request undefined
   */
  AccountRefreshToken(request: RefreshTokenRequest): __Observable<JwtAuthentificationResponse> {
    return this.AccountRefreshTokenResponse(request).pipe(
      __map(_r => _r.body as JwtAuthentificationResponse)
    );
  }

  /**
   * @param createUserRequest undefined
   */
  AccountCreateResponse(createUserRequest: CreateUserRequest): __Observable<__StrictHttpResponse<UserResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = createUserRequest;
    let req = new HttpRequest<any>(
      'POST',
      this.rootUrl + `/api/Account/Create`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<UserResponse>;
      })
    );
  }
  /**
   * @param createUserRequest undefined
   */
  AccountCreate(createUserRequest: CreateUserRequest): __Observable<UserResponse> {
    return this.AccountCreateResponse(createUserRequest).pipe(
      __map(_r => _r.body as UserResponse)
    );
  }
  AccountGetAllResponse(): __Observable<__StrictHttpResponse<GetAllUserResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Account/GetAll`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<GetAllUserResponse>;
      })
    );
  }  AccountGetAll(): __Observable<GetAllUserResponse> {
    return this.AccountGetAllResponse().pipe(
      __map(_r => _r.body as GetAllUserResponse)
    );
  }

  /**
   * @param Id undefined
   */
  AccountGetByIdResponse(Id?: null | string): __Observable<__StrictHttpResponse<UserResponse>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (Id != null) __params = __params.set('Id', Id.toString());
    let req = new HttpRequest<any>(
      'GET',
      this.rootUrl + `/api/Account/GetById`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'json'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<UserResponse>;
      })
    );
  }
  /**
   * @param Id undefined
   */
  AccountGetById(Id?: null | string): __Observable<UserResponse> {
    return this.AccountGetByIdResponse(Id).pipe(
      __map(_r => _r.body as UserResponse)
    );
  }

  /**
   * @param user undefined
   */
  AccountUpdateResponse(user: UpdateUserRequest): __Observable<__StrictHttpResponse<string>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    __body = user;
    let req = new HttpRequest<any>(
      'PUT',
      this.rootUrl + `/api/Account/Update`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'text'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return _r as __StrictHttpResponse<string>;
      })
    );
  }
  /**
   * @param user undefined
   */
  AccountUpdate(user: UpdateUserRequest): __Observable<string> {
    return this.AccountUpdateResponse(user).pipe(
      __map(_r => _r.body as string)
    );
  }

  /**
   * @param id undefined
   */
  AccountDeleteResponse(id?: null | string): __Observable<__StrictHttpResponse<boolean>> {
    let __params = this.newParams();
    let __headers = new HttpHeaders();
    let __body: any = null;
    if (id != null) __params = __params.set('id', id.toString());
    let req = new HttpRequest<any>(
      'DELETE',
      this.rootUrl + `/api/Account/Delete`,
      __body,
      {
        headers: __headers,
        params: __params,
        responseType: 'text'
      });

    return this.http.request<any>(req).pipe(
      __filter(_r => _r instanceof HttpResponse),
      __map((_r) => {
        return (_r as HttpResponse<any>).clone({ body: (_r as HttpResponse<any>).body === 'true' }) as __StrictHttpResponse<boolean>
      })
    );
  }
  /**
   * @param id undefined
   */
  AccountDelete(id?: null | string): __Observable<boolean> {
    return this.AccountDeleteResponse(id).pipe(
      __map(_r => _r.body as boolean)
    );
  }
}

module AccountService {
}

export { AccountService }
