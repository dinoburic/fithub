import {NgModule} from '@angular/core';

import {AuthRoutingModule} from './auth-routing-module';
import {AuthLayoutComponent} from './auth-layout/auth-layout.component';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import {ForgotPasswordComponent} from './forgot-password/forgot-password.component';
import {LogoutComponent} from './logout/logout.component';
import {SharedModule} from '../shared/shared-module';
import { PasswordStrengthMeterComponent } from './register/password-strength-meter/password-strength-meter.component';
import { RecaptchaModule } from 'ng-recaptcha';
import { FacebookLoginProvider, GoogleLoginProvider, GoogleSigninButtonModule, SocialAuthService, SocialAuthServiceConfig, SocialLoginModule } from '@abacritt/angularx-social-login';


@NgModule({
  declarations: [
    AuthLayoutComponent,
    LoginComponent,
    RegisterComponent,
    ForgotPasswordComponent,
    LogoutComponent,
    PasswordStrengthMeterComponent
  ],
  imports: [
    AuthRoutingModule,
    SharedModule,
    RecaptchaModule,
    GoogleSigninButtonModule
  ]
})
export class AuthModule { }
