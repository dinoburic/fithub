// users-api.service.ts
import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { GetUserByIdQueryDto, UpdateUserCommand } from './users-api.models';

@Injectable({
  providedIn: 'root',
})
export class UsersApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/Users`;
  private http = inject(HttpClient);

  /**
   * GET /Users/{id}
   */
  getById(id: number): Observable<GetUserByIdQueryDto> {
    return this.http.get<GetUserByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  /**
   * PUT /Users/{id}
   */
  update(id: number, payload: UpdateUserCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

 
}