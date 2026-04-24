import { Component, inject } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { FitnessCentersService } from '../../../api-services/fitness-centers/fitness-centers-api.service';
import { ListFitnessCentersQueryDto } from '../../../api-services/fitness-centers/fitness-centers-api.model';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';
import { Router } from '@angular/router';
import { RegisterCommand } from '../../../api-services/auth/auth-api.model';
import { BaseComponent } from '../../../core/components/base-classes/base-component';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-register',
  standalone: false,
  templateUrl: './register.component.html',
  styleUrl: './register.component.scss',
})
export class RegisterComponent extends BaseComponent {
  private fb = inject(FormBuilder);
  private auth = inject(AuthFacadeService);
  private router = inject(Router);
  private fitnessCentersService = inject(FitnessCentersService);
    private toaster = inject(ToasterService);

  fitnessCenters: ListFitnessCentersQueryDto[] = [];
  
  captchaToken: string|null = null;

  resolved(token: string | null) {
    this.captchaToken = token;
     
  }
  
    ngOnInit(): void {
      this.loadFitnessCenters();
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
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    gender: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    fitnessCenter: [null, Validators.required],
    isTrainer: [false],
    });

  onSubmit(): void {
    if (this.form.invalid || this.isLoading) return;

    this.startLoading();

    const isTrainer = this.form.value.isTrainer ?? false;
    const roleID = isTrainer ? 3 : 1;

    if(this.captchaToken) {
        const payload:any = {
          dto : {
            name: this.form.value.firstName ?? '',
            surname: this.form.value.lastName ?? '',
            gender: this.form.value.gender == "Male" ? true : false,
            email: this.form.value.email ?? '',
            password: this.form.value.password ?? '',
            centerID: Number(this.form.value.fitnessCenter) ?? 0,
            phoneNumber: '001230000', 
            roleID: roleID,
            captchaToken: this.captchaToken
          }
      }
    

      this.auth.register(payload).subscribe({
        next: () => {
          this.router.navigate(['/auth/login']);
        },
        error: (err) => {
          this.isLoading = false;
          this.errorMessage =
            err?.error?.message ?? 'Registration failed. Please try again.';
          },
        complete: () => {
          this.stopLoading();
        }
      });
    }
  }

}
