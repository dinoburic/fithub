import { NgModule } from '@angular/core';

import { PublicRoutingModule } from './public-routing-module';
import { PublicLayoutComponent } from './public-layout/public-layout.component';
import { SearchProductsComponent } from './search-products/search-products.component';
import { SharedModule } from '../shared/shared-module';
import { ProgramsCatalogComponent } from './programs-catalog/programs-catalog.component';
import { FitnessPlanCardComponent } from "../shared/components/fitness-plan-card/fitness-plan-card/fitness-plan-card.component";
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BlogListComponent } from './blog/blog-list/blog-list.component';
import { BlogDetailComponent } from './blog/blog-detail/blog-detail.component';
import { AiChatComponent } from '../shared/components/ai-chat/ai-chat.component';
import { MatIconModule } from '@angular/material/icon';
import { ProgramDetailsComponent } from './program-details/program-details.component';
import { ProgramReviewsComponent } from './program-reviews/program-reviews.component';
import { UpperCasePipe } from '@angular/common';

@NgModule({
  declarations: [
    PublicLayoutComponent,
    SearchProductsComponent,
    ProgramsCatalogComponent,
    LandingPageComponent,
    BlogListComponent,
    BlogDetailComponent,
    ProgramDetailsComponent,
    ProgramReviewsComponent
  ],
  imports: [
    SharedModule,
    PublicRoutingModule,
    FitnessPlanCardComponent,
    MatIconModule,
    UpperCasePipe
  ]
})
export class PublicModule { }
