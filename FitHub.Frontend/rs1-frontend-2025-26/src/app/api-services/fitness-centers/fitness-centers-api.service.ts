import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ListFitnessCentersRequest,
  ListFitnessCentersResponse,
  ListFitnessCentersQueryDto
} from './fitness-centers-api.model';
import { buildHttpParams } from '../../core/models/build-http-params';


@Injectable({
  providedIn: 'root',
})
export class FitnessCentersService {
  private readonly baseUrl = `${environment.apiUrl}/api/FitnessCenters`;
  private http = inject(HttpClient);
  
    /**
     * GET /FitnessCenters
     */
    list(request?: ListFitnessCentersRequest): Observable<ListFitnessCentersResponse> {
      const params = request ? buildHttpParams(request as any) : undefined;
  
      return this.http.get<ListFitnessCentersResponse>(this.baseUrl, {
        params,
      });
    }
  
}
