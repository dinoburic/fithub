import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CartPageComponent } from './cart/cart.component';
import { ClientLayoutComponent } from './client-layout/client-layout.component';
import { LandingPageComponent } from '../public/landing-page/landing-page.component';
import { CheckoutPageComponent } from './checkout/checkout.component';
import { myAuthGuard } from '../../core/guards/my-auth-guard';
import { OrderSuccessPageComponent } from './order-success-page/order-success-page.component';
import { WishlistPageComponent } from './wishlist-page/wishlist-page.component';
import { SettingsComponent } from './settings/settings.component';
import { DashboardComponent } from './dashboard/dashboard.component';

const routes: Routes = [
  {
    path: 'order-success/:id',
    loadComponent: () => import('./order-success-page/order-success-page.component')
      .then(m => m.OrderSuccessPageComponent)
  },
  {
      path: '',
      component: ClientLayoutComponent,
      children: [
        {
          path: 'home',
          component: LandingPageComponent,
        },
        {
          path: 'cart',
          component: CartPageComponent,
        },
        
  
      
        {
          path: 'settings',
          component: SettingsComponent,
        },
        {
          path: 'dashboard',
          component: DashboardComponent,
        },
  
        {
          path: 'cart',
          component: CartPageComponent,
        },
       
        {
          path: 'wishlist',
          component: WishlistPageComponent,
        },
        {
              path: 'checkout',
              component: CheckoutPageComponent,
              canActivate: [myAuthGuard],
              
            }, 

            {
              path: 'order-success/:id', 
              loadComponent: () => import('./order-success-page/order-success-page.component')
                .then(m => m.OrderSuccessPageComponent)
            },
                  
        // default client route → /admin/products
        {
          path: '',
          redirectTo: 'home',
          pathMatch: 'full',
        },
      ],
    },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ClientRoutingModule { }
