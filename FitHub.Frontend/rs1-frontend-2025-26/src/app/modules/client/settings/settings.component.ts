import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';
import { UsersApiService } from '../../../api-services/users/users-api.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { GetUserByIdQueryDto, UpdateUserCommand } from '../../../api-services/users/users-api.models';


@Component({
  selector: 'app-settings',
  standalone: false,
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.scss',
})
export class SettingsComponent implements OnInit {
  settingsForm!: FormGroup;
  isSaved: boolean = false;
  currentUserId: number | undefined;

  private fb = inject(FormBuilder);
  private authFacade = inject(AuthFacadeService);
  private usersService = inject(UsersApiService);
  private toaster = inject(ToasterService);

  ngOnInit(): void {
    const user = this.authFacade.currentUser();
    this.currentUserId = user?.userId;

    this.settingsForm = this.fb.group({
      id: [this.currentUserId], 
      name: ['', [Validators.required, Validators.maxLength(50)]],
      surname: ['', [Validators.required, Validators.maxLength(50)]],
      phoneNumber: ['', [Validators.pattern('^[0-9+ ]*$')]],
      address: ['', [Validators.maxLength(100)]],
      weight: [null],
      height: [null]
    });

    if (this.currentUserId) {
      this.loadUserProfile();
    }
  }

  loadUserProfile(): void {
    this.usersService.getById(this.currentUserId!).subscribe({
      next: (userData: GetUserByIdQueryDto) => {
        this.settingsForm.patchValue({
          id: userData.id,
          name: userData.name,
          surname: userData.surname,
          phoneNumber: userData.phoneNumber,
          address: userData.address,
          weight: userData.weight,
          height: userData.height
        });
      },
      error: (err) => this.toaster.error('Error', err)
    });
  }

  onSubmit(): void {
    if (this.settingsForm.valid && this.currentUserId) {
      const updateData = this.settingsForm.value as UpdateUserCommand;

      this.usersService.update(this.currentUserId, updateData).subscribe({
        next: () => {
          this.isSaved = true;
          setTimeout(() => this.isSaved = false, 3000); 
        },
        error: (err) => {
          this.toaster.error('Error while saving profile data', err);
          this.toaster.error('Došlo je do greške pri spašavanju!');
        }
      });
    } else {
      this.settingsForm.markAllAsTouched();
    }
  }
}
