import { Component, inject, OnInit } from '@angular/core';
import { FitnessPlansService } from '../../../api-services/fitness-plans/fitness-plans-api.service';
import { FilterGroup, FilterOption } from '../../shared/models/filter.model';
import { FilterGroupComponent } from '../../shared/components/filter-group/filter-group.component';
import { Router } from '@angular/router';
import { AuthFacadeService } from '../../../core/services/auth/auth-facade.service';
import { ListFitnessPlansQueryDto } from '../../../api-services/fitness-plans/fitness-plans-api.model';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-programs-catalog',
  standalone: false,
  templateUrl: './programs-catalog.component.html',
  styleUrl: './programs-catalog.component.scss',
})
export class ProgramsCatalogComponent implements OnInit {
  orderBy = 'relevance';
  fitnessPlansService = inject(FitnessPlansService);
  private authService = inject(AuthFacadeService);
  private toaster = inject(ToasterService);

  recommendedPlans: ListFitnessPlansQueryDto[] = [];
  isLoadingRecommendations = false;
  filterConfigs: FilterGroup[] = [
    {
      title: 'Difficulty',
      icon: 'fa-solid fa-bolt',
      options: [
        { label: 'Beginner', value: 'Beginner', checked: false },
        { label: 'Intermediate', value: 'Intermediate', checked: false },
        { label: 'Advanced', value: 'Advanced', checked: false }
      ],
      type: 'checkbox'
    },
    {
      title: 'Fitness Centre',
      icon: 'fa-solid fa-building',
      options: [
        { label: 'Arena fitness centar', value: 1, checked: false },
        { label: 'Perfect Fit Gym & Fitness', value: 2, checked: false }
      ],
      type: 'checkbox'
    },
     {
      title: 'Price Range',
      icon: 'fa-solid fa-tag',
      options: [
        { label: '$0 - $50', value: '0-50', checked: false },
        { label: '$51 - $100', value: '51-100', checked: false },
        { label: '$101 - $150', value: '101-150', checked: false }
      ],
      type: 'checkbox'
    }
  ];

  allWorkouts: ListFitnessPlansQueryDto[] = [];
  filteredWorkouts: ListFitnessPlansQueryDto[] = [];

  searchQuery = '';
  page=1;
  private router = inject(Router);

  ngOnInit(): void {
    if (this.authService.isAuthenticated()) {
      this.loadRecommendations();
    }
    this.loadWorkouts();
  }

  loadWorkouts() {
    this.fitnessPlansService.list({
      orderBy: this.orderBy,
      paging: {
       page: this.page,
      pageSize: 12
  }
    }).subscribe({
      next: plans => {
        this.allWorkouts = plans.items;
         
        this.applyFilters();
      },
      error: err => this.toaster.error('Error loading plans', err)
    });
  }

  loadRecommendations(): void {
    this.isLoadingRecommendations = true;
    this.fitnessPlansService.getRecommendations().subscribe({
      next: (plans) => {
        this.recommendedPlans = plans;
        this.isLoadingRecommendations = false;
         
      },
      error: (err) => {
        this.toaster.error('Error loading recommendations', err);
        this.isLoadingRecommendations = false;
      }
    });
  }

  openProgram(id: number) {
   
  this.router.navigate(['/program-details',id]);
}

  applyFilters() {
   const difficultyFilters = this.filterConfigs[0].options
     .filter((o: FilterOption) => o.checked)
     .map((o: FilterOption) => o.value)
     .filter((value): value is string => typeof value === 'string');
   const activeCentres = this.filterConfigs[1].options
     .filter((o: FilterOption) => o.checked)
     .map((o: FilterOption) => o.value)
     .filter((value): value is number => typeof value === 'number');
   const priceFilters = this.filterConfigs[2].options.filter((o:FilterOption) => o.checked).map((o:FilterOption) => o.value);

    this.filteredWorkouts = this.allWorkouts.filter(workout => {
      const difficultyMatch =
        difficultyFilters.length === 0 || (workout.difficulty !== null && difficultyFilters.includes(workout.difficulty));
      const centreMatch =
        activeCentres.length === 0 || activeCentres.includes(workout.centerId);

      const searchMatch =
        this.searchQuery === '' ||
        workout.title.toLowerCase().includes(this.searchQuery.toLowerCase());

      const priceMatch =
        priceFilters.length === 0 ||
        priceFilters.some(range => {
          const [min, max] = String(range).split('-').map(Number);
          return workout.price >= min && workout.price <= max;
        });

      return centreMatch && searchMatch && difficultyMatch && priceMatch;
    });
  }

  onSearch(event: Event) {
    const target = event.target as HTMLInputElement;
    this.searchQuery = target.value.toLowerCase();
    this.applyFilters();
  }

  onOrderChange(e: Event) {
  const target = e.target as HTMLSelectElement;
  this.orderBy = target.value;
   
  this.loadWorkouts();
}
}
