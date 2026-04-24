import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

import { PublicLayoutComponent } from './public-layout/public-layout.component';
import { SearchProductsComponent } from './search-products/search-products.component';
import { ProgramsCatalogComponent } from './programs-catalog/programs-catalog.component';
import { LandingPageComponent } from './landing-page/landing-page.component';
import { BlogListComponent } from './blog/blog-list/blog-list.component';
import { BlogDetailComponent } from './blog/blog-detail/blog-detail.component';
import { ProgramDetailsComponent } from './program-details/program-details.component';

const routes: Routes = [
  {
    path: '',
    component: PublicLayoutComponent,
    children: [
      {
        path: '',
        component: LandingPageComponent,
        pathMatch: 'full'
      },
      {
        path: 'programs',
        component: ProgramsCatalogComponent,
        pathMatch: 'full'
      },
      // later, it can also be like this:
      // { path: 'about', component: AboutComponent },
      // { path: 'contact', component: ContactComponent },
      {
        path: 'blog',
        component: BlogListComponent,
        pathMatch: 'full'
      },
      {
        path: 'blog/:id',
        component: BlogDetailComponent,
        pathMatch: 'full'
      },
      {
        path: 'program-details/:id',
        component: ProgramDetailsComponent,
        pathMatch: 'full'
      },

      { path: '**', redirectTo: '' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class PublicRoutingModule { }
