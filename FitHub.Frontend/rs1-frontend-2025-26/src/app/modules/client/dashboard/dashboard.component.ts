import { Component, inject, OnInit } from '@angular/core';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';

// API Servisi
import { OrdersApiService } from '../../../api-services/orders/orders-api.service';
import { CartApiService } from '../../../api-services/cart/cart-api.service';
import { ListOrdersResponse } from '../../../api-services/orders/orders-api.models';
import { GetMyCartResponse } from '../../../api-services/cart/cart-api.models';

@Component({
  selector: 'app-dashboard',
  standalone: false,
  templateUrl: './dashboard.component.html',
  styleUrl: './dashboard.component.scss',
})
export class DashboardComponent implements OnInit {
  userName: string = 'User';
  totalOrders: number = 0;
  cartItemsCount: number = 0;
  favoritesCount: number = 0;

  private authFacade = inject(AuthFacadeService);
  private ordersService = inject(OrdersApiService);
  private cartService = inject(CartApiService);

  ngOnInit(): void {
    const user = this.authFacade.currentUser();
    if (user?.email) {
      this.userName = user.email.split('@')[0];
    }

    this.loadDashboardData();
  }

  loadDashboardData(): void {
    // All HTTP calls were moved to services
    this.ordersService.list().subscribe({
      next: (res: ListOrdersResponse) => {
        this.totalOrders = res.items.length;
      },
      error: () => { this.totalOrders = 0; }
    });

    this.cartService.loadCart().subscribe({
      next: (res: GetMyCartResponse) => {
        this.cartItemsCount = res.cartItems.length;
      },
      error: () => { this.cartItemsCount = 0; }
    });

   
  }
}
