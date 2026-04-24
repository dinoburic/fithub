import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ListCommentsRequest,
  ListCommentsResponse,
  GetCommentByIdQueryDto,
  CreateCommentCommand,
  UpdateCommentCommand
} from './comments-api.models';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({
  providedIn: 'root'
})
export class CommentsApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/Comments`;
  private http = inject(HttpClient);

  /**
   * GET /Comments
   * List comments with optional query parameters.
   */
  list(request?: ListCommentsRequest): Observable<ListCommentsResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;
    return this.http.get<ListCommentsResponse>(this.baseUrl, { params });
  }

  /**
   * GET /Comments/{id}
   * Get a single comment by ID.
   */
  getById(id: number): Observable<GetCommentByIdQueryDto> {
    return this.http.get<GetCommentByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  /**
   * POST /Comments
   * Create a new comment.
   * @returns ID of the newly created comment
   */
  create(payload: CreateCommentCommand): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(this.baseUrl, payload);
  }

  /**
   * PUT /Comments/{id}
   * Update an existing comment.
   */
  update(id: number, payload: UpdateCommentCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

  /**
   * DELETE /Comments/{id}
   * Delete a comment (soft delete).
   */
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
