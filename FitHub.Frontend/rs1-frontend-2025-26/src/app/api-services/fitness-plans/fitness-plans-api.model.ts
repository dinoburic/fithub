import { BasePagedQuery } from "../../core/models/paging/base-paged-query";
import { PageResult } from "../../core/models/paging/page-result";

export class ListFitnessPlansRequest extends BasePagedQuery {
  search?: string | null;
  centerID?: number | null;
  fitnessPlanTypeID?: number | null;
  difficulty?: string | null;

  minPrice?: number | null;
  maxPrice?: number | null;

  createdFrom?: string | null; // ISO date
  createdTo?: string | null;

  orderBy?: string | null;
}

export interface ListFitnessPlansQueryDto {
  planID: number;
  title: string;
  description: string|null;
  difficulty:string|null;
  price:number;
  dailyDurationInMinutes:number|null;
  durationInWeeks:number|null;
  reviewsNumber:number|null;
  averageRating:number|null;
  centerId:number;
  fitnessPlanTypeID:number;
  userID:number;

}

export type ListFitnessPlansResponse = PageResult<ListFitnessPlansQueryDto>;

export interface GetFitnessPlanByIdQueryDto {
  planID: number,
  title: string,
  description: string,
  difficulty: string,
  price: number,
  dailyDurationInMinute: number,
  durationInWeeks: number,
  reviewsNumber: number,
  averageRating: number,
  centerID: number,
  fitnessPlanTypeID: number,
  userID: number,
  createdAtUtc: string
}


