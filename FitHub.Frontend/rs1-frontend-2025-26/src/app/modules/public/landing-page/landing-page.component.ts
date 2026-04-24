import { Component, OnInit } from '@angular/core';
import { inject } from '@angular/core';
import { FitnessPlansService } from '../../../api-services/fitness-plans/fitness-plans-api.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ListFitnessPlansQueryDto, ListFitnessPlansResponse } from '../../../api-services/fitness-plans/fitness-plans-api.model';
import { ListFitnessCentersQueryDto } from '../../../api-services/fitness-centers/fitness-centers-api.model';
import { ToasterService } from '../../../core/services/toaster.service';


@Component({
  selector: 'app-landing-page',
  standalone: false,
  templateUrl: './landing-page.component.html',
  styleUrl: './landing-page.component.scss',
})
export class LandingPageComponent implements OnInit {
  
openProgram(id: number) {
   
  this.router.navigate(['/program-details',id]);
}
 
private fitnessPlansService = inject(FitnessPlansService);
private router = inject(Router);
private toaster = inject(ToasterService);
private route = inject(ActivatedRoute);
  currentYear: string = "2026";

  fitnessPlans: ListFitnessPlansQueryDto[] = [];

  ngOnInit(): void {
    this.fitnessPlansService.list().subscribe({
      next: (response) => {
        this.fitnessPlans = response.items;
      },
      error: (error) => {
        this.toaster.error('Error fetching fitness plans:', error);
      }
    });
  }

}
 
 
 
 