import { Component, inject, Input, OnInit } from '@angular/core';
import { ReviewsApiService } from '../../../api-services/reviews/reviews-api.service';
import { GetReviewsByPlanIdQueryDto, ListReviewsResponse } from '../../../api-services/reviews/reviews-api.models';
import { ToasterService } from '../../../core/services/toaster.service';

@Component({
  selector: 'app-program-reviews',
  standalone: false,
  templateUrl: './program-reviews.component.html',
  styleUrl: './program-reviews.component.scss',
})
export class ProgramReviewsComponent implements OnInit {
  @Input() planId!: number;
  @Input() centerId!: number; 

  private reviewsService = inject(ReviewsApiService);
  private toaster = inject(ToasterService);

  reviews: GetReviewsByPlanIdQueryDto[] = [];
  
  newComment: string = '';
  newRating: number = 0;
  hoverRating: number = 0; 
  
  stars: number[] = [1, 2, 3, 4, 5];
  isSubmitting = false;

  ngOnInit(): void {
    if (this.planId) {
      this.loadReviews();
    }
  }

  loadReviews(): void {
    this.reviewsService.getByPlanId(this.planId).subscribe({
      next: (data) => this.reviews = data,
      error: (err) => this.toaster.error('Error loading reviews', err)
    });
   
  }

  setRating(rating: number): void {
    this.newRating = rating;
  }

  submitReview(): void {
    if (this.newRating === 0) {
      this.toaster.error('Please choose rating');
      return;
    }

    this.isSubmitting = true;
    const request = {
      rating: this.newRating,
      comment: this.newComment,
      fitnessPlanID: this.planId
    };

  

    this.reviewsService.create(request).subscribe({
      next: () => {
      this.loadReviews(); 
     this.newComment = '';
      this.newRating = 0;
      this.hoverRating = 0;
      this.isSubmitting = false;
      },
      error: (err) => {
        this.toaster.error('Error', err);
        this.toaster.error(err.error?.message || 'Error submitting review. Are you sure you purchased the program?');
        this.isSubmitting = false;
      }
    });
  }
  getArray(count: number): number[] {
    return Array(Math.floor(count)).fill(0);
  }
}
