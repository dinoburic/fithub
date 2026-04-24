import { BasePagedQuery } from "../../core/models/paging/base-paged-query";
import { PageResult } from "../../core/models/paging/page-result";

export class ListFitnessCentersRequest extends BasePagedQuery {
  search?: string | null;
}

export interface ListFitnessCentersQueryDto {
  id: number;
  name: string;
}

export type ListFitnessCentersResponse = PageResult<ListFitnessCentersQueryDto>;
