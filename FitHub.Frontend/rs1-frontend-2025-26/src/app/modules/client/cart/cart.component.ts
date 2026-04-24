import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { forkJoin, of } from 'rxjs';
import { map, catchError } from 'rxjs/operators';
import { CartApiService } from '../../../api-services/cart/cart-api.service';
import { CartItemViewModel, GetMyCartItemQueryDto } from '../../../api-services/cart/cart-api.models';
import { FitnessPlansService } from '../../../api-services/fitness-plans/fitness-plans-api.service';
import { CartItemComponent } from '../../shared/components/cart-item/cart-item.component';
import { RouterLink } from '@angular/router';
import { ToasterService } from '../../../core/services/toaster.service';
import { DialogHelperService } from '../../shared/services/dialog-helper.service';
import { DialogButton } from '../../shared/models/dialog-config.model';

@Component({
  selector: 'app-cart-page',
  standalone: true,
  imports: [CommonModule, CartItemComponent, RouterLink],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.scss'
})
export class CartPageComponent implements OnInit {
  private fitnessPlansService = inject(FitnessPlansService);
  private cartService = inject(CartApiService);
  private toaster = inject(ToasterService);
  private dialog = inject(DialogHelperService);

  cartItems: CartItemViewModel[] = [];
  savedItems: CartItemViewModel[] = [];
  isLoading = true;

  ngOnInit(): void {
    this.loadCart();
  }

  private loadCart(): void {
    this.isLoading = true;

    this.cartService.loadCart().subscribe({
      next: (response) => {
        if (response && response.cartItems.length > 0) {
          const activeRaw = response.cartItems.filter(x => !x.isSavedForLater);
          const savedRaw = response.cartItems.filter(x => x.isSavedForLater);
          this.loadFitnessPlanDetails(activeRaw, 'cart');
          this.loadFitnessPlanDetails(savedRaw, 'saved');

          if (activeRaw.length === 0) {
            this.cartItems = [];
            this.isLoading = false;
          }
        } else {
          this.cartItems = [];
          this.savedItems = [];
          this.isLoading = false;
        }
      },
      error: (err) => {
        this.toaster.error('Failed to load cart:', err);
        this.isLoading = false;
      }
    });
  }

  private loadFitnessPlanDetails(items: GetMyCartItemQueryDto[], target: 'cart' | 'saved'): void {
    if (items.length === 0) {
      if (target === 'cart') this.cartItems = [];
      if (target === 'saved') this.savedItems = [];
      return;
    }

    const planRequests = items.map(item =>
      this.fitnessPlansService.getByID(item.fitnessPlanID).pipe(
        map(plan => ({
          cartItemID: item.cartItemID,
          fitnessPlanID: item.fitnessPlanID,
          price: item.price,
          quantity: item.quantity,
          title: plan.title,
          isSavedForLater: item.isSavedForLater
        })),
        catchError(() => {
          this.toaster.error('Error loading plan:', item.fitnessPlanID);
          return of(null);
        })
      )
    );

    forkJoin(planRequests).subscribe({
      next: (results) => {
        const validItems = results.filter(item => item !== null) as CartItemViewModel[];

        if (target === 'cart') {
          this.cartItems = validItems;
          this.isLoading = false;
        } else {
          this.savedItems = validItems;
        }
      },
      error: (err) => {
        this.toaster.error('Failed to load plan details:', err);
        if (target === 'cart') this.isLoading = false;
      }
    });
  }

  saveForLater(item: CartItemViewModel): void {
    this.isLoading = true;
    this.cartService.saveForLater(item.cartItemID).subscribe({
      next: () => {
        this.loadCart();
      },
      error: (err) => {
        this.toaster.error('Error saving for later', err);
        this.isLoading = false;
      }
    });
  }

  moveToCart(item: CartItemViewModel): void {
    this.isLoading = true;
    this.cartService.moveBackToCart(item.cartItemID).subscribe({
      next: () => {
        this.loadCart();
      },
      error: (err) => {
        this.toaster.error('Error moving back to cart', err);
        this.isLoading = false;
      }
    });
  }

  remove(item: CartItemViewModel): void {
    this.dialog.confirmDelete(`Are you sure you want to remove "${item.title}" from your cart?`).subscribe({
      next: (result) => {
        if (result?.button !== DialogButton.DELETE) {
          return;
        }

        this.cartService.deleteFromCart(item.cartItemID).subscribe({
          next: () => {
            this.loadCart();
            this.toaster.success('Item removed successfully.');
          },
          error: (err) => {
            this.toaster.error('Failed to remove item:', err);
          }
        });
      }
    });
  }

  clearAll(): void {
    if (this.cartItems.length === 0) return;

    this.dialog.confirmDelete('Are you sure you want to clear ALL items from your cart? This action cannot be undone.').subscribe({
      next: (result) => {
        if (result?.button !== DialogButton.DELETE) {
          return;
        }

        const deleteRequests = this.cartItems.map(item =>
          this.cartService.deleteFromCart(item.cartItemID).pipe(
            catchError(err => {
              this.toaster.error('Error deleting item:', err);
              return of(null);
            })
          )
        );

        forkJoin(deleteRequests).subscribe({
          next: () => {
            this.cartItems = [];
            this.toaster.success('Cart cleared.');
          }
        });
      }
    });
  }

  get subtotal(): number {
    return this.cartItems.reduce(
      (sum, item) => sum + item.price * item.quantity,
      0
    );
  }
}
