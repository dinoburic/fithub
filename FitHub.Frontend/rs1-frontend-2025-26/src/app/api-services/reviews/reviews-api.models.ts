import { BasePagedQuery } from "../../core/models/paging/base-paged-query";
import { PageResult } from "../../core/models/paging/page-result";



export class ListReviewsRequest extends BasePagedQuery {
  search?: string | null;
  fitnessPlanID?: number | null;
  centerID?: number | null;
}

export interface ListReviewsQueryDto {
  reviewID: number;
  rating: number;
  comment?: string | null;
  fitnessPlanID: number;
  userName: string;
  createdAtUTC: string;
}

export interface GetReviewByIdQueryDto {
  reviewID: number;
  rating: number;
  comment?: string | null;
  fitnessPlanID: number;
  planTitle: string;
  userID: number;
  userName: string;
  createdAtUTC: string;
}

/**
 * Paged response for GET /Reviews
 */
export type ListReviewsResponse = PageResult<ListReviewsQueryDto>;

/**
 * Command for POST /Reviews
 * Corresponds to: CreateReviewCommand.cs
 */
export interface CreateReviewCommand {
  rating: number;
  comment?: string | null;
  fitnessPlanID: number;
}

/**
 * Command for PUT /Reviews/{id}
 * Corresponds to: UpdateReviewCommand.cs
 */
export interface UpdateReviewCommand {
  rating: number;
  comment?: string | null;
}

export interface GetReviewsByPlanIdQueryDto {
  reviewID: number;
  rating: number;
  comment?: string | null;
  fitnessPlanID: number;
  userID: number;
  userName: string;
  createdAtUTC: string;
}