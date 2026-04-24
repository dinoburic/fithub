import { NgModule, provideBrowserGlobalErrorListeners, provideZoneChangeDetection } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { provideAnimations} from '@angular/platform-browser/animations';
import {HttpClient, provideHttpClient, withInterceptors} from '@angular/common/http';
import { SocialAuthServiceConfig, GoogleLoginProvider, FacebookLoginProvider, SocialLoginModule } from '@abacritt/angularx-social-login';
import { AppRoutingModule } from './app-routing-module';
import { AppComponent } from './app.component';
import {authInterceptor} from './core/interceptors/auth-interceptor.service';
import {loadingBarInterceptor} from './core/interceptors/loading-bar-interceptor.service';
import {errorLoggingInterceptor} from './core/interceptors/error-logging-interceptor.service';
import {TranslateLoader, TranslateModule} from '@ngx-translate/core';
import {CustomTranslateLoader} from './core/services/custom-translate-loader';
import {materialModules} from './modules/shared/material-modules';
import {SharedModule} from './modules/shared/shared-module';
import { RECAPTCHA_SETTINGS, RecaptchaModule, RecaptchaSettings } from 'ng-recaptcha';
import {NgxStripeModule} from 'ngx-stripe';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    RecaptchaModule,
    BrowserModule,
    SocialLoginModule,
    NgxStripeModule.forRoot('pk_test_51T1vPo2LCbuFvsAXVoOtTWqpcBJtnVlqhR0bIRWzGaR5v7p0QbbJW6Z5ZfuO6QRbesyg571t1lFpqukGvvCCFxFW003rPy1Jb2'),
    AppRoutingModule,
    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: (http: HttpClient) => new CustomTranslateLoader(http),
        deps: [HttpClient]
      }
    }),
    SharedModule,
    materialModules,
  ],
  providers: [
    {
      provide: RECAPTCHA_SETTINGS,
      useValue: { siteKey: '6LehBTcsAAAAADyEOgNjsjRuE6bgtileinGp0eY9' } as RecaptchaSettings,
    },
    
    {
      provide: 'SocialAuthServiceConfig', // <-- I konfiguraciju prebacujemo ovdje!
      useValue: {
        autoLogin: false,
        providers: [
          {
            id: GoogleLoginProvider.PROVIDER_ID,
            provider: new GoogleLoginProvider('638693312621-rinh0mhb00cj633infu3n96nsf0nccp2.apps.googleusercontent.com')
          },
          {
            id: FacebookLoginProvider.PROVIDER_ID,
            provider: new FacebookLoginProvider('1420849872479873')
          }
        ],
        onError: (err) => {
          
        }
      } as SocialAuthServiceConfig,
    },
    provideAnimations(),
    provideBrowserGlobalErrorListeners(),
    provideZoneChangeDetection(),
    provideHttpClient(
      withInterceptors([
        loadingBarInterceptor,
        authInterceptor,
        errorLoggingInterceptor
      ])
    )
  ],
  exports: [
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
