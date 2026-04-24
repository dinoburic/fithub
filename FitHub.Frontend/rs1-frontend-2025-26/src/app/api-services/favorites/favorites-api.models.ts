export interface GetMyWishlistQueryDto {
  wishListItemID: number;
  fitnessPlanID: number;
  planTitle: string;
  price: number;
  difficulty?: string | null;
  addedAt: string;
}