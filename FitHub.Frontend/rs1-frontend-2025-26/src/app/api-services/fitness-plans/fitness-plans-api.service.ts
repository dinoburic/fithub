import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ListFitnessPlansRequest,
  ListFitnessPlansResponse,
  ListFitnessPlansQueryDto
} from './fitness-plans-api.model';
import { buildHttpParams } from '../../core/models/build-http-params';


@Injectable({
  providedIn: 'root',
})
export class FitnessPlansService {
  private readonly baseUrl = `${environment.apiUrl}/api/FitnessPlans`;
  private http = inject(HttpClient);
  
    /**
     * GET /FitnessPlans
     */
    list(request?: ListFitnessPlansRequest): Observable<ListFitnessPlansResponse> {
      const params = request ? buildHttpParams(request as any) : undefined;
  
      return this.http.get<ListFitnessPlansResponse>(this.baseUrl, {
        params,
      });
    }

    getByID(planID: number): Observable<ListFitnessPlansQueryDto> {
      return this.http.get<ListFitnessPlansQueryDto>(`${this.baseUrl}/${planID}`);
    }

    getRecommendations(): Observable<ListFitnessPlansQueryDto[]> {
    return this.http.get<ListFitnessPlansQueryDto[]>(`${this.baseUrl}/recommendations`);
  }
  
}