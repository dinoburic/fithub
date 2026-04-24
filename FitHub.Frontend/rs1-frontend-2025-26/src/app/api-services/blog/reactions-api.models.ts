import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

// === QUERIES (READ) ===

/**
 * Query parameters for GET /Reactions
 * Corresponds to: ListReactionsQuery.cs
 */
export class ListReactionsRequest extends BasePagedQuery {
  blogPostId?: number | null;
  userId?: number | null;
  type?: string | null;
}

/**
 * Response item for GET /Reactions
 * Corresponds to: ReactionListItemDto.cs
 */
export interface ReactionListItemDto {
  id: number;
  blogPostId: number;
  userId: number;
  userName: string;
  type: string;
  dateTimeAddedUtc: string;
}

/**
 * Paged response for GET /Reactions
 */
export type ListReactionsResponse = PageResult<ReactionListItemDto>;

/**
 * Summary response for GET /Reactions/summary/{blogPostId}
 */
export interface ReactionSummaryDto {
  blogPostId: number;
  totalReactions: number;
  byType: { [key: string]: number };
}

// === COMMANDS (WRITE) ===

/**
 * Command for POST /Reactions
 * Corresponds to: CreateReactionCommand.cs
 */
export interface CreateReactionCommand {
  blogPostId: number;
  userId: number;
  type: string; // 'like' | 'love' | 'insightful' | 'celebrate'
}
