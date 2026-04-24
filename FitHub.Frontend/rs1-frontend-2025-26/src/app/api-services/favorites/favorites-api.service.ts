import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { GetMyWishlistQueryDto } from './favorites-api.models';


@Injectable({
  providedIn: 'root',
})
export class FavoritesService {
  private readonly baseUrl = `${environment.apiUrl}/api/Wishlists`;
  private http = inject(HttpClient);

  getMyWishlist(): Observable<GetMyWishlistQueryDto[]> {
    return this.http.get<GetMyWishlistQueryDto[]>(this.baseUrl);
  }


  add(fitnessPlanId: number): Observable<number> {
    return this.http.post<number>(`${this.baseUrl}/${fitnessPlanId}`, {});
  }

  remove(fitnessPlanId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${fitnessPlanId}`);
  }
}