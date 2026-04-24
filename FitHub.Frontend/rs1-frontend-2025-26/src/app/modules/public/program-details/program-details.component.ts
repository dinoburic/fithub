import { Component, OnInit, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { FitnessPlansService } from '../../../api-services/fitness-plans/fitness-plans-api.service';
import { ListFitnessPlansQueryDto } from '../../../api-services/fitness-plans/fitness-plans-api.model'; 
import { ToasterService } from '../../../core/services/toaster.service';
import { CartApiService } from '../../../api-services/cart/cart-api.service';
import { AddToCartCommand, GetMyCartItemQueryDto, GetMyCartResponse } from '../../../api-services/cart/cart-api.models';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';
import { FavoritesService } from '../../../api-services/favorites/favorites-api.service';
import { GetMyWishlistQueryDto } from '../../../api-services/favorites/favorites-api.models';

@Component({
  selector: 'app-program-details',
  standalone: false,
  templateUrl: './program-details.component.html',
  styleUrls: ['./program-details.component.scss']
})
export class ProgramDetailsComponent implements OnInit {
 
  program: ListFitnessPlansQueryDto | null = null;
  private authService=inject(AuthFacadeService);
  relatedPrograms: ListFitnessPlansQueryDto[] = [];
  private toaster = inject(ToasterService);
  private cartService = inject(CartApiService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  private fitnessPlansService = inject(FitnessPlansService);
  private modal = inject(ToasterService);
  private wishlistsService = inject(FavoritesService);

  placeholderImage = '/images/planphoto.jpg'; 
  mockReviews = [
    { userName: 'Sarah M.', userAvatar: 'images/review1.png', rating: 5, text: 'Odličan program za početnike!' },
    { userName: 'Emily R.', userAvatar: 'images/review2.png', rating: 4, text: 'Vježbe su super, ali mi fali malo više kardija.' }
  ];

   planId!: number;

  ngOnInit(): void {
     this.planId = Number(this.route.snapshot.paramMap.get('id'));

    if (this.planId) {
      this.loadProgram(this.planId);
      this.checkIfFavorite();
      this.checkIfInCart();
    }
  }

  isFavorite!: boolean;
  isInCart: boolean = false;

  checkIfInCart(): void {
    this.cartService.loadCart().subscribe({
      next: (response: GetMyCartResponse) => {
        const cartItems = response?.cartItems || [];
        this.isInCart = cartItems.some((item: GetMyCartItemQueryDto) => item.fitnessPlanID === this.planId);
      },
      error: (err) => this.toaster.error('Error checking cart status:', err)
    });
  }

  checkIfFavorite(): void {
    this.wishlistsService.getMyWishlist().subscribe({
      next: (wishlist: GetMyWishlistQueryDto[]) => {
        this.isFavorite = wishlist.some((item: GetMyWishlistQueryDto) => item.fitnessPlanID === this.planId);
      }
    });
  }

  toggleWishlist(): void {
    if (this.isFavorite) {
      this.wishlistsService.remove(this.planId).subscribe({
        next: () => {
          this.isFavorite = false;
          this.toaster.success("Fitness plan removed from wishlist");
         },
        error: (err) => this.toaster.error('Error while removing plan from wishlist', err)
      });
    } else {
      this.wishlistsService.add(this.planId).subscribe({
        next: () => {this.isFavorite = true;
          this.toaster.success("Fitness plan added to wishlist");
        },
        error: (err) => {
          this.toaster.error('Error while adding plan to wishlist', err);
          this.toaster.error("Error while adding fitness plan to wishlist", err);
        }
      });
    }
  }

  loadProgram(id: number): void {
     
    this.fitnessPlansService.getByID(id).subscribe({
      next: (data) => {
        this.program = data;
      },
      error: (err) => {
        this.toaster.error('Error loading fitness program:', err);
        this.toaster.error("Error while fetching program");
      }
    });
  }

  getStars(rating: number | null): number[] {
    if (!rating) return [];
    return Array(Math.floor(rating)).fill(0);
  }

  onAddToCart() {
    if (!this.authService.isAuthenticated()) {
      this.router.navigate(['/auth/login']);
      return;
    }

    if (this.isInCart) {
      this.modal.info('This fitness plan is already in your cart.');
      return;
    }

    const payload: AddToCartCommand = {
      fitnessPlanID: this.program!.planID,
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
