import { Component, OnInit } from '@angular/core';
import { AuthenticationHelper } from 'src/app/shared/helpers/auth.helper';
import { LoginRequest } from 'src/app/swagger/shared/models';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {

  public loginRequest: LoginRequest;
  constructor(private authenticationHelper: AuthenticationHelper) {
    this.loginRequest = {
      email: null,
      password: null,
      isRememberMe: false
    }
   }

  ngOnInit() {

  }

  onLogInClick() {
    this.authenticationHelper.login(this.loginRequest);
  }
}
