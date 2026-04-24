import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

// === QUERIES (READ) ===

/**
 * Query parameters for GET /BlogPosts
 * Corresponds to: ListBlogPostsQuery.cs
 */
export class ListBlogPostsRequest extends BasePagedQuery {
  search?: string | null;
  categoryId?: number | null;
  userId?: number | null;
}

/**
 * Response item for GET /BlogPosts
 * Corresponds to: BlogPostListItemDto.cs
 */
export interface BlogPostListItemDto {
  id: number;
  title: string;
  contentPreview?: string | null;
  categoryId: number;
  categoryTitle: string;
  userId: number;
  authorName: string;
  createdAtUtc: string;
}

/**
 * Response for GET /BlogPosts/{id}
 * Corresponds to: GetBlogPostByIdQueryDto.cs
 */
export interface GetBlogPostByIdQueryDto {
  id: number;
  title: string;
  content?: string | null;
  categoryId: number;
  categoryTitle: string;
  userId: number;
  authorName: string;
  createdAtUtc: string;
  updatedAtUtc?: string | null;
  commentsCount: number;
}

/**
 * Paged response for GET /BlogPosts
 */
export type ListBlogPostsResponse = PageResult<BlogPostListItemDto>;

// === COMMANDS (WRITE) ===

/**
 * Command for POST /BlogPosts
 * Corresponds to: CreateBlogPostCommand.cs
 */
export interface CreateBlogPostCommand {
  title: string;
  content?: string | null;
  categoryId: number;
  userId: number;
}

/**
 * Command for PUT /BlogPosts/{id}
 * Corresponds to: UpdateBlogPostCommand.cs
 */
export interface UpdateBlogPostCommand {
  title: string;
  content?: string | null;
  categoryId: number;
}
