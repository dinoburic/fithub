import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';

// API Servisi
import { OrdersApiService } from '../../../api-services/orders/orders-api.service';
import { CartApiService } from '../../../api-services/cart/cart-api.service';
import { PaymentApiService } from '../../../api-services/payment/payment-api.service';

// Modeli i Core servisi
import { CartItemViewModel } from '../../../api-services/cart/cart-api.models';
import { CreateOrderCommand } from '../../../api-services/orders/orders-api.models';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss'
})
export class CheckoutPageComponent implements OnInit {
  private fb = inject(FormBuilder);
  private router = inject(Router);
  private toaster = inject(ToasterService);
  
  // Architecturally correct: call exclusively through services 
  private orderService = inject(OrdersApiService);
  private cartService = inject(CartApiService);
  private paymentService = inject(PaymentApiService);

  checkoutForm!: FormGroup;
  isSubmitting = false; // Loading mehanizam 
  totalAmount = 0;

  ngOnInit(): void {
    this.initForm();
    this.loadCartData();
  }

  private initForm(): void {
    this.checkoutForm = this.fb.group({
      firstName: ['', [Validators.required, Validators.minLength(2)]],
      lastName: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      address: ['', [Validators.required, Validators.minLength(5)]],
      city: ['', Validators.required],
      zipCode: ['', Validators.required],
      country: ['', Validators.required]
    });
  }

  private loadCartData(): void {
    this.cartService.loadCart().subscribe({
      next: (res) => {
        if (res && res.cartItems) {
          const active = res.cartItems.filter(x => !x.isSavedForLater && !x.isDeleted);
          this.totalAmount = active.reduce((acc, curr) => acc + (curr.price * curr.quantity), 0);
          
          if (active.length === 0) {
            this.toaster.error('Your cart is empty!');
            this.router.navigate(['/cart']);
          }
        }
      }
    });
  }

  onSubmit(): void {
    if (this.checkoutForm.invalid) {
      this.checkoutForm.markAllAsTouched();
      return;
    }
  
    // Lock form 
    this.isSubmitting = true;
    const command: CreateOrderCommand = this.checkoutForm.value;
  
    this.orderService.create(command).subscribe({
      next: (newOrder) => {
        this.processPayment(newOrder.id);
      },
      error: () => {
        this.toaster.error('Error creating order.');   
        this.isSubmitting = false;
      }
    });
  }

  /**
   * Centralized logic for payment initialization through Payment service 
   */
  private processPayment(orderId: number): void {
    this.paymentService.createCheckoutSession(orderId).subscribe({
      next: (stripeResponse) => {
        if (stripeResponse.url) {
          // Preusmjeravanje na Stripe
          window.location.href = stripeResponse.url;
        } else {
          this.toaster.error('Stripe payment initialization failed.');
          this.isSubmitting = false;
        }
      },
      error: () => {
        this.toaster.error('Error initializing payment. Please try again later.');
        this.isSubmitting = false;
      }
    });
  }
}
