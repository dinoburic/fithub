import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { buildHttpParams } from '../../core/models/build-http-params';
import { CreateReviewCommand, GetReviewByIdQueryDto, GetReviewsByPlanIdQueryDto, ListReviewsRequest, ListReviewsResponse } from './reviews-api.models';


@Injectable({
  providedIn: 'root'
})
export class ReviewsApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/Reviews`;
  private http = inject(HttpClient);

  list(request?: ListReviewsRequest): Observable<ListReviewsResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;

    return this.http.get<ListReviewsResponse>(this.baseUrl, {
      params,
    });
  }

  getById(id: number): Observable<GetReviewByIdQueryDto> {
    return this.http.get<GetReviewByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  create(payload: CreateReviewCommand): Observable<number> {
    return this.http.post<number>(this.baseUrl, payload);
  }

  getByPlanId(planId: number): Observable<GetReviewsByPlanIdQueryDto[]> { 
    return this.http.get<GetReviewsByPlanIdQueryDto[]>(`${this.baseUrl}/plan/${planId}`);
  }
}
