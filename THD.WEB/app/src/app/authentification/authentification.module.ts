import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthentificationRoutingModule } from './authentification-routing.module';
import { LoginComponent } from './login/login.component';
import { AuthenticationComponent } from './authentication.component';


@NgModule({
  declarations: [LoginComponent, AuthenticationComponent],
  imports: [
    FormsModule,
    CommonModule,
    AuthentificationRoutingModule
  ]
})
export class AuthentificationModule { }
