import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ListReactionsRequest,
  ListReactionsResponse,
  ReactionSummaryDto,
  CreateReactionCommand
} from './reactions-api.models';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({
  providedIn: 'root'
})
export class ReactionsApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/Reactions`;
  private http = inject(HttpClient);

  /**
   * GET /Reactions
   * List reactions with optional query parameters.
   */
  list(request?: ListReactionsRequest): Observable<ListReactionsResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;
    return this.http.get<ListReactionsResponse>(this.baseUrl, { params });
  }

  /**
   * GET /Reactions/summary/{blogPostId}
   * Get reaction counts grouped by type for a blog post.
   */
  getSummary(blogPostId: number): Observable<ReactionSummaryDto> {
    return this.http.get<ReactionSummaryDto>(`${this.baseUrl}/summary/${blogPostId}`);
  }

  /**
   * POST /Reactions
   * Create or update a reaction.
   * @returns ID of the reaction
   */
  create(payload: CreateReactionCommand): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(this.baseUrl, payload);
  }

  /**
   * DELETE /Reactions/{id}
   * Remove a reaction.
   */
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
