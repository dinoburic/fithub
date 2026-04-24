import { PageResult } from '../../core/models/paging/page-result';
import { BasePagedQuery } from '../../core/models/paging/base-paged-query';

// === QUERIES (READ) ===

/**
 * Query parameters for GET /Orders
 * Corresponds to: ListOrdersQuery.cs
 */
export class GetMyCartRequest extends BasePagedQuery {
  constructor() {
    super();
  }
}

export interface CartItem {
    cartItemID:number;
    fitnessPlanID:number;
    price:number;
    quantity:number;
}


/**
 * User info in list response
 */
export interface GetMyCartQueryDto {
  cartID:number;
  userID:number;
  centerID:number;
  createdAt:string;
  isDeleted:boolean;
  subTotal:number;
  cartItems:GetMyCartItemQueryDto[];
}


export interface GetMyCartItemQueryDto {
    cartItemID:number;
    fitnessPlanID:number;
    price:number;
    quantity:number;
    isDeleted: boolean;
    isSavedForLater: boolean | 'false';
}

export interface CartItemViewModel {
  cartItemID: number;
  fitnessPlanID: number;
  price: number;
  quantity: number;

  title: string;
  durationInWeeks?: number;
  difficulty?: string;
  imageUrl?: string;
}




export type GetMyCartResponse = GetMyCartQueryDto;

// === COMMANDS (WRITE) ===

/**
 * Order item for create command
 */
export interface AddToCartCommand {
  fitnessPlanID: number;
  quantity: number;
}

export interface AddToCartCommandDto {
    cartID:number;
    cartItemID: number;
    fitnessPlanID:number;
}

export interface RemoveFromCartCommand {
    cartItemID: number;
}

export interface SaveForLaterCommand {
    cartItemID: number;
}

export interface MoveBackToCartCommand {
    cartItemID: number;
}