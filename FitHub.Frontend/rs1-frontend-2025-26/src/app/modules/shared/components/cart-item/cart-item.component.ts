import { CommonModule } from "@angular/common";
import { Component, EventEmitter, inject, Input, Output } from "@angular/core";
import { CartItemViewModel } from "../../../../api-services/cart/cart-api.models";
import { CartApiService } from "../../../../api-services/cart/cart-api.service";

@Component({
  selector: 'app-cart-item',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './cart-item.component.html',
  styleUrl: './cart-item.component.scss'
})
export class CartItemComponent {
    private cartApi = inject(CartApiService);
    @Input({required:true}) item!: CartItemViewModel;
    @Input() isSaved: boolean = false;

    @Output() remove=new EventEmitter<number>();
    @Output() saveForLater = new EventEmitter<number>();
    @Output() moveToCart = new EventEmitter<number>();

    onRemove() {
    this.cartApi.deleteFromCart(this.item.cartItemID);
    this.cartApi.loadCart().subscribe(() => {
        this.remove.emit(this.item.cartItemID);
      });
  }

  onSaveForLater() {
    this.cartApi.saveForLater(this.item.cartItemID);
    this.cartApi.loadCart().subscribe(() => {
        this.saveForLater.emit(this.item.cartItemID);
      });
  }
  onMoveToCart() {
    this.cartApi.moveBackToCart(this.item.cartItemID);
    this.cartApi.loadCart().subscribe(() => {
        this.moveToCart.emit(this.item.cartItemID);
      });
  }
}