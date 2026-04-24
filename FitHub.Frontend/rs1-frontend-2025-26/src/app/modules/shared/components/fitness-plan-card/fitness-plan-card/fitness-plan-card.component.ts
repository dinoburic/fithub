import { Component, inject, Input } from '@angular/core';
import { AuthFacadeService } from '../../../../../core/services/auth/auth-facade.service';
import { CartApiService } from '../../../../../api-services/cart/cart-api.service';
import { Router } from '@angular/router';
import { AddToCartCommand } from '../../../../../api-services/cart/cart-api.models';
import { ToasterService } from '../../../../../core/services/toaster.service';
import { take } from 'rxjs';
import { FavoritesService } from '../../../../../api-services/favorites/favorites-api.service';
import { CommonModule } from '@angular/common';
import { ListFitnessPlansQueryDto } from '../../../../../api-services/fitness-plans/fitness-plans-api.model';
import { GetMyWishlistQueryDto } from '../../../../../api-services/favorites/favorites-api.models';

@Component({
  selector: 'app-fitness-plan-card',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './fitness-plan-card.component.html',
  styleUrl: './fitness-plan-card.component.scss',
})
export class FitnessPlanCardComponent {
  @Input({ required: true }) fitnessPlan!: ListFitnessPlansQueryDto;
  private authService = inject(AuthFacadeService);
  private cartService = inject(CartApiService);
  private router = inject(Router);
  private modal = inject(ToasterService);
  private toaster = inject(ToasterService);
  private wishlistsService = inject(FavoritesService);

  isFavorite: boolean = false;
  isInCart: boolean = false;

  ngOnInit(): void {
   if (this.authService.isAuthenticated() && this.fitnessPlan) {
      this.checkIfFavorite();
      this.checkIfInCart();
    }
  }

  checkIfInCart(): void {
    this.cartService.getCartItems()
      .pipe(take(1))
      .subscribe({
        next: (items) => {
          this.isInCart = items.some(item => item.fitnessPlanID === this.fitnessPlan.planID);
        },
        error: (err) => this.toaster.error('Error checking cart status:', err)
      });
  }

  checkIfFavorite(): void {
    this.wishlistsService.getMyWishlist().subscribe({
      next: (wishlist: GetMyWishlistQueryDto[]) => {
        this.isFavorite = wishlist.some((item: GetMyWishlistQueryDto) => item.fitnessPlanID === this.fitnessPlan.planID);
      }
    });
  }

  toggleWishlist(event: Event): void {
    event.stopPropagation();

    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/auth/login']);
      return;
    }

    if (this.isFavorite) {
      this.wishlistsService.remove(this.fitnessPlan.planID).subscribe({
        next: () => {
          this.isFavorite = false;
          this.modal.success("Removed from wishlist"); 
        },
        error: (err) => this.toaster.error('Error removing from wishlist', err)
      });
    } else {
      this.wishlistsService.add(this.fitnessPlan.planID).subscribe({
        next: () => {
          this.isFavorite = true;
          this.modal.success("Added to wishlist");
        },
        error: (err) => {
          this.toaster.error('Error adding to wishlist', err);
          this.modal.error("Failed to add to wishlist");
        }
      });
    }
  }

 addToCart() {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/auth/login']);
      return;
    }

    if (this.isInCart) {
      this.modal.info('This fitness plan is already in your cart.');
      return;
    }

    const payload: AddToCartCommand = {
      fitnessPlanID: this.fitnessPlan.planID,
      quantity: 1
    };

    this.cartService.addToCart(payload).subscribe({
      next: (cartItemId) => {
         
        this.modal.success('Fitness plan added to cart!');
        this.isInCart = true;
      },
      error: (error) => {
        this.toaster.error('❌ Error adding fitness plan to cart:', error);
        this.modal.error('Failed to add fitness plan to cart.');
      }
    });
  }

  
}
