import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  ListBlogPostsRequest,
  ListBlogPostsResponse,
  GetBlogPostByIdQueryDto,
  CreateBlogPostCommand,
  UpdateBlogPostCommand
} from './blog-posts-api.models';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({
  providedIn: 'root'
})
export class BlogPostsApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/BlogPosts`;
  private http = inject(HttpClient);

  /**
   * GET /BlogPosts
   * List blog posts with optional query parameters.
   */
  list(request?: ListBlogPostsRequest): Observable<ListBlogPostsResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;
    return this.http.get<ListBlogPostsResponse>(this.baseUrl, { params });
  }

  /**
   * GET /BlogPosts/{id}
   * Get a single blog post by ID.
   */
  getById(id: number): Observable<GetBlogPostByIdQueryDto> {
    return this.http.get<GetBlogPostByIdQueryDto>(`${this.baseUrl}/${id}`);
  }

  /**
   * POST /BlogPosts
   * Create a new blog post.
   * @returns ID of the newly created blog post
   */
  create(payload: CreateBlogPostCommand): Observable<{ id: number }> {
    return this.http.post<{ id: number }>(this.baseUrl, payload);
  }

  /**
   * PUT /BlogPosts/{id}
   * Update an existing blog post.
   */
  update(id: number, payload: UpdateBlogPostCommand): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/${id}`, payload);
  }

  /**
   * DELETE /BlogPosts/{id}
   * Delete a blog post (soft delete).
   */
  delete(id: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${id}`);
  }
}
