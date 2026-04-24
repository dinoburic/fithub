import { CommonModule } from '@angular/common';
import { Component, EventEmitter, Input, Output } from '@angular/core';
import { GetMyWishlistQueryDto } from '../../../api-services/favorites/favorites-api.models'; 

@Component({
  selector: 'app-wishlist-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './wishlist-item.component.html',
  styleUrl: './wishlist-item.component.scss'
})
export class WishlistItemComponent {
  @Input({ required: true }) item!: GetMyWishlistQueryDto;
  @Input() isInCart: boolean = false; 

  @Output() remove = new EventEmitter<number>();
  @Output() addToCart = new EventEmitter<number>();

  onRemove() {
    this.remove.emit(this.item.fitnessPlanID);
  }

  onAddToCart() {
    if (!this.isInCart) {
      this.addToCart.emit(this.item.fitnessPlanID);
    }
  }
}