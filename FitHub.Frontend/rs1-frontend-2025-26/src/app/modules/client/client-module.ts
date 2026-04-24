import {NgModule} from '@angular/core';

import {ClientRoutingModule} from './client-routing-module';
import {SharedModule} from '../shared/shared-module';
import { ClientLayoutComponent } from './client-layout/client-layout.component';
import { PublicRoutingModule } from '../public/public-routing-module';
import { FitnessPlanCardComponent } from '../shared/components/fitness-plan-card/fitness-plan-card/fitness-plan-card.component';
import { materialModules } from '../shared/material-modules';
import { CheckoutPageComponent } from './checkout/checkout.component';
import { OrderSuccessPageComponent } from './order-success-page/order-success-page.component';
import { WishlistItemComponent } from './wishlist-item/wishlist-item.component';
import { WishlistPageComponent } from './wishlist-page/wishlist-page.component';
import { SettingsComponent } from './settings/settings.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DashboardComponent } from './dashboard/dashboard.component';


@NgModule({
  declarations: [
    ClientLayoutComponent,
    SettingsComponent,
    DashboardComponent
  ],
  imports: [
    SharedModule,
    ClientRoutingModule,
    FitnessPlanCardComponent,
    CheckoutPageComponent,
    ReactiveFormsModule,
    OrderSuccessPageComponent,
    ...materialModules
  ]
})
export class ClientModule { }
