import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { forkJoin, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { WishlistItemComponent } from '../wishlist-item/wishlist-item.component'; // Adjust path
import { FavoritesService } from '../../../api-services/favorites/favorites-api.service';
import { CartApiService } from '../../../api-services/cart/cart-api.service';
import { ToasterService } from '../../../core/services/toaster.service';
import { GetMyWishlistQueryDto } from '../../../api-services/favorites/favorites-api.models'; 
import { DialogHelperService } from '../../shared/services/dialog-helper.service';
import { GetMyCartItemQueryDto, GetMyCartResponse } from '../../../api-services/cart/cart-api.models';
import { DialogButton } from '../../shared/models/dialog-config.model';

@Component({
  selector: 'app-wishlist-page',
  standalone: true,
  imports: [CommonModule, WishlistItemComponent],
  templateUrl: './wishlist-page.component.html',
  styleUrl: './wishlist-page.component.scss'
})
export class WishlistPageComponent implements OnInit {
  private wishlistsService = inject(FavoritesService);
  private cartService = inject(CartApiService);
  private toaster = inject(ToasterService);
  private dialog = inject(DialogHelperService);

  wishlistItems: GetMyWishlistQueryDto[] = [];
  cartPlanIds: number[] = []; 
  isLoading = true;

  ngOnInit(): void {
    this.loadData();
  }

  private loadData(): void {
    this.isLoading = true;

    forkJoin({
      wishlist: this.wishlistsService.getMyWishlist().pipe(catchError(() => of([] as GetMyWishlistQueryDto[]))),
      cart: this.cartService.loadCart().pipe(catchError(() => of({
        cartID: 0,
        userID: 0,
        centerID: 0,
        createdAt: '',
        isDeleted: false,
        subTotal: 0,
        cartItems: []
      } as GetMyCartResponse)))
    }).subscribe({
      next: (res: { wishlist: GetMyWishlistQueryDto[]; cart: GetMyCartResponse }) => {
        this.wishlistItems = res.wishlist;
        this.cartPlanIds = res.cart.cartItems.map((c: GetMyCartItemQueryDto) => c.fitnessPlanID);
        
        this.isLoading = false;
      },
      error: () => this.isLoading = false
    });
  }

  checkIfInCart(planId: number): boolean {
    return this.cartPlanIds.includes(planId);
  }

 onRemoveFromWishlist(planId: number): void {
    this.dialog.confirmDelete('Are you sure you want to remove this plan from your wishlist?').subscribe({
      next: (result) => {
        if (result?.button !== DialogButton.DELETE) {
          return;
        }

        this.wishlistsService.remove(planId).subscribe({
          next: () => {
            this.wishlistItems = this.wishlistItems.filter(item => item.fitnessPlanID !== planId);
            this.toaster.success('Removed from wishlist');
          },
          error: () => {
            this.toaster.error('Failed to remove item');
          }
        });
      }
    });
  }

  onAddToCart(planId: number): void {
    const payload = { fitnessPlanID: planId, quantity: 1 };
    
    this.cartService.addToCart(payload).subscribe({
      next: () => {
        this.cartPlanIds.push(planId);
        this.toaster.success('Added to cart!');
      },
      error: (err) => {
        this.toaster.error('Failed to add to cart:', err);
        this.toaster.error('Failed to add to cart.');
      }
    });
  }
}
