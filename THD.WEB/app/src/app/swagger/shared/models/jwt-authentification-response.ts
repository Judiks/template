/* tslint:disable */
export interface JwtAuthentificationResponse {
  id?: string;
  fullName?: string;
  accessToken?: string;
  refreshToken?: string;
  roles?: Array<string>;
}
