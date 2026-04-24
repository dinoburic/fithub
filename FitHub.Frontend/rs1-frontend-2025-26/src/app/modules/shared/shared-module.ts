import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {FitPaginatorBarComponent} from './components/fit-paginator-bar/fit-paginator-bar.component';
import {materialModules} from './material-modules';
import {FormsModule, ReactiveFormsModule} from '@angular/forms';
import {TranslatePipe} from '@ngx-translate/core';
import { FitConfirmDialogComponent } from './components/fit-confirm-dialog/fit-confirm-dialog.component';
import {DialogHelperService} from './services/dialog-helper.service';
import { FitLoadingBarComponent } from './components/fit-loading-bar/fit-loading-bar.component';
import { FitTableSkeletonComponent } from './components/fit-table-skeleton/fit-table-skeleton.component';
import { FithubFooterComponent } from './components/fithub-footer/fithub-footer/fithub-footer.component';
import { FithubNavbarComponent } from './components/fithub-navbar/fithub-navbar/fithub-navbar.component';
import { RouterModule } from '@angular/router';
import { FitnessPlanCardComponent } from './components/fitness-plan-card/fitness-plan-card/fitness-plan-card.component';
import { FilterGroupComponent } from './components/filter-group/filter-group.component';
import { SavedForLaterItemComponent } from './components/saved-for-later-item/saved-for-later-item.component';
import { AiChatComponent } from './components/ai-chat/ai-chat.component';



@NgModule({
  declarations: [
    FitPaginatorBarComponent,
    FitConfirmDialogComponent,
    FitLoadingBarComponent,
    FitTableSkeletonComponent,
    FithubFooterComponent,
    FithubNavbarComponent,
    FilterGroupComponent,
    SavedForLaterItemComponent,
    AiChatComponent
  ],
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    TranslatePipe,
    RouterModule,
    FormsModule,
    FitnessPlanCardComponent,
    ...materialModules
  ],
  providers: [
    DialogHelperService
  ],
  exports:[
    FitPaginatorBarComponent,
    CommonModule,
    ReactiveFormsModule,
    TranslatePipe,
    FormsModule,
    FitLoadingBarComponent,
    FitTableSkeletonComponent,
    FithubFooterComponent,
    FithubNavbarComponent,
    RouterModule,
    AiChatComponent,
    FilterGroupComponent,
    FitnessPlanCardComponent,
    ...materialModules
  ]
})
export class SharedModule { }
