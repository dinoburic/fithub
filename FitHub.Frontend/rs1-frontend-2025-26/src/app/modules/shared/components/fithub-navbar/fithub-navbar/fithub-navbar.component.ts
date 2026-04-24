import { Component, inject } from '@angular/core';
import { AuthApiService } from '../../../../../api-services/auth/auth-api.service';
import { AuthFacadeService } from '../../../../../core/services/auth/auth-facade.service';
import { CartApiService } from '../../../../../api-services/cart/cart-api.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-fithub-navbar',
  standalone: false,
  templateUrl: './fithub-navbar.component.html',
  styleUrl: './fithub-navbar.component.scss',
})
export class FithubNavbarComponent {
  private authApi = inject(AuthFacadeService);
  isAuthenticated = this.authApi.isAuthenticated;
  currentUser = this.authApi.currentUser;
  private cartService = inject(CartApiService);
  private router = inject(Router);

  cartCount$ = this.cartService.getCartItemsCount();

  logout(): void {
 
  localStorage.removeItem('jwt_token'); 
  localStorage.removeItem('refresh_token');
  this.authApi.logout();
  this.router.navigate(['/']);
}
  
}
