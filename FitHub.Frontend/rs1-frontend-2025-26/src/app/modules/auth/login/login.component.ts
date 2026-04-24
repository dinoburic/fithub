import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { BaseComponent } from '../../../core/components/base-classes/base-component';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';
import { ExternalLoginCommand, LoginCommand } from '../../../api-services/auth/auth-api.model';
import { CurrentUserService } from '../../../core/services/auth/current-user.service';
import { FitnessCentersService } from '../../../api-services/fitness-centers/fitness-centers-api.service';
import { ListFitnessCentersQueryDto } from '../../../api-services/fitness-centers/fitness-centers-api.model';
import { FacebookLoginProvider, SocialAuthService } from '@abacritt/angularx-social-login';
import { AuthApiService } from '../../../api-services/auth/auth-api.service';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-login',
  standalone: false,
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent extends BaseComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthFacadeService);
  private router = inject(Router);
  private currentUser = inject(CurrentUserService);
  private fitnessCentersService = inject(FitnessCentersService);
  private socialAuthService = inject(SocialAuthService);
    private toaster = inject(ToasterService);
  hidePassword = true;

  fitnessCenters: ListFitnessCentersQueryDto[] = [];

  ngOnInit(): void {
    this.loadFitnessCenters();
    this.socialAuthService.authState.subscribe((user) => {
      if (user) {
        const tokenToSend = user.provider === 'GOOGLE' ? user.idToken : user.authToken;
        
        // Pack into DTO object
        const command: ExternalLoginCommand = {
          provider: user.provider!,
          idToken: tokenToSend!
        };
        
        // Call facade with object
        this.auth.externalLogin(command).subscribe({
          next: () => {
            
            this.router.navigate(['/programs']);
          },
          error: (err) => {
            this.toaster.error('Login failed:', err);
          }
        });
      }
    });
  }

  signInWithFB(): void {
  this.socialAuthService.signIn(FacebookLoginProvider.PROVIDER_ID);
}

  loadFitnessCenters(): void {
    this.fitnessCentersService.list().subscribe({
      next: (data) => {
        this.fitnessCenters = data.items;
      },
      error: (err) => {
        this.toaster.error('Error loading fitness centers');
        this.toaster.error('Status:', err.status);
        this.toaster.error('Message:', err.message);
        this.toaster.error('Error body:', err.error);
      }
    });
  }

  

  form = this.fb.group({
    email: ['string@string.com', [Validators.required, Validators.email]],
    password: ['@String123', [Validators.required]],
    fitnessCenters: [null, Validators.required],
    rememberMe: [false],
  });

  onSubmit(): void {
    if (this.form.invalid || this.isLoading) return;

    this.startLoading();

    const payload: LoginCommand = {
      email: this.form.value.email ?? '',
      password: this.form.value.password ?? '',
      fitnessCenterId: Number(this.form.value.fitnessCenters ?? 0),
      fingerprint: null,
    };

    this.auth.login(payload).subscribe({
      next: () => {
        this.stopLoading();
        const target = this.currentUser.getDefaultRoute();
       
        this.router.navigate([target]);
      },
      error: (err) => {
        this.stopLoading('Invalid credentials. Please try again.');
        this.toaster.error('Login error:', err);
      },
    });
  }
}
