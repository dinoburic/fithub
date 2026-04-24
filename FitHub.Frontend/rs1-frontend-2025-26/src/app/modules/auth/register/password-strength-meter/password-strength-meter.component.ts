import { Component, Input } from '@angular/core';

@Component({
  selector: 'app-password-strength-meter',
  standalone: false,
  templateUrl: './password-strength-meter.component.html',
  styleUrl: './password-strength-meter.component.scss',
})
export class PasswordStrengthMeterComponent {
  @Input() password: string = '';

  get strength() {
    let s = 0;
    if (this.password.length >= 8) s++;
    if (/[A-Z]/.test(this.password)) s++;
    if (/[0-9]/.test(this.password)) s++;
    if (/[^A-Za-z0-9]/.test(this.password)) s++;
    return s;
  }

  get label() {
    if (this.password.length < 8) return 'Too short';
    return ['Weak', 'Fair', 'Good', 'Strong'][this.strength - 1] ?? '';
  }

  get percent() {
    if (this.password.length < 8) return 10;
    return this.strength * 25;
  }

  get levelClass() {
    if (this.password.length < 8) return 'short';
    return ['weak', 'fair', 'good', 'strong'][this.strength - 1] ?? 'weak';
  }

}
