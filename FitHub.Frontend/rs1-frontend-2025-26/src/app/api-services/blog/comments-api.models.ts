import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

// === QUERIES (READ) ===

/**
 * Query parameters for GET /Comments
 * Corresponds to: ListCommentsQuery.cs
 */
export class ListCommentsRequest extends BasePagedQuery {
  blogPostId?: number | null;
  userId?: number | null;
}

/**
 * Response item for GET /Comments
 * Corresponds to: CommentListItemDto.cs
 */
export interface CommentListItemDto {
  id: number;
  blogPostId: number;
  userId: number;
  authorName: string;
  content: string;
  createdAtUtc: string;
}

/**
 * Response for GET /Comments/{id}
 * Corresponds to: GetCommentByIdQueryDto.cs
 */
export interface GetCommentByIdQueryDto {
  id: number;
  blogPostId: number;
  blogPostTitle: string;
  userId: number;
  authorName: string;
  content: string;
  createdAtUtc: string;
}

/**
 * Paged response for GET /Comments
 */
export type ListCommentsResponse = PageResult<CommentListItemDto>;

// === COMMANDS (WRITE) ===

/**
 * Command for POST /Comments
 * Corresponds to: CreateCommentCommand.cs
 */
export interface CreateCommentCommand {
  blogPostId: number;
  userId: number;
  content: string;
}

/**
 * Command for PUT /Comments/{id}
 * Corresponds to: UpdateCommentCommand.cs
 */
export interface UpdateCommentCommand {
  content: string;
}
