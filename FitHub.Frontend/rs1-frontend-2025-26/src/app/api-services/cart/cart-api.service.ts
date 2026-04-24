import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, map, Observable, tap } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  AddToCartCommand,
  GetMyCartItemQueryDto,
  GetMyCartQueryDto,
  GetMyCartRequest,
  GetMyCartResponse
} from './cart-api.models';
import { buildHttpParams } from '../../core/models/build-http-params';

@Injectable({
  providedIn: 'root'
})
export class CartApiService {

  private readonly baseUrl = `${environment.apiUrl}/api/Cart`;
  private http = inject(HttpClient);

  private cartItems$ = new BehaviorSubject<GetMyCartItemQueryDto[]>([]);

  getCartItems() {
    return this.cartItems$.asObservable().pipe(
      map(items => items.filter(item => !item.isDeleted && !item.isSavedForLater))
    );
  }

  getCartItemsCount(): Observable<number> {
    return this.cartItems$.asObservable().pipe(
      // 1. Prvo filtriramo (koristimo map, ne tap!)
      map(items => items.filter(item => !item.isDeleted && !item.isSavedForLater)), 
      // 2. Then take the filtered list length
      map(filteredItems => filteredItems.length)
    );
  }

  getSavedForLaterCount(): Observable<number> {
    return this.cartItems$.asObservable().pipe(
      // 1. Prvo filtriramo (koristimo map, ne tap!)
      map(items => items.filter(item => !item.isDeleted && item.isSavedForLater)), 
      // 2. Then take the filtered list length
      map(filteredItems => filteredItems.length)
    );
  }

 loadCart(request?: GetMyCartRequest): Observable<GetMyCartResponse> {
  const params = request ? buildHttpParams(request as any) : undefined;

  return this.http.get<GetMyCartResponse>(this.baseUrl, { params }).pipe(
    tap(cart => {
      if (cart.cartItems && cart.cartItems.length > 0 ) {
        this.cartItems$.next(cart.cartItems); 
      } else {
        this.cartItems$.next([]);  
      }
    })
  );
}


getSavedForLaterItems(): Observable<GetMyCartItemQueryDto[]> {
  return this.cartItems$.asObservable().pipe(
    map(items => items.filter(item => !item.isDeleted && item.isSavedForLater))
  );
}

  getMyCart(request?: GetMyCartRequest): Observable<GetMyCartResponse> {
    const params = request ? buildHttpParams(request as any) : undefined;
    return this.http.get<GetMyCartResponse>(this.baseUrl, { params });
  }

  addToCart(payload: AddToCartCommand): Observable<number> {
    return this.http.post<number>(this.baseUrl, payload).pipe(
      tap(() => this.loadCart().subscribe())
    );
  }

  deleteFromCart(cartItemId: number): Observable<void> {
    return this.http.delete<void>(`${this.baseUrl}/${cartItemId}`).pipe(
      tap(() => this.loadCart().subscribe())
    );
  }

  saveForLater(cartItemId: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/save-for-later/${cartItemId}`, null).pipe(
      tap(() => this.loadCart().subscribe())
    );
  }

  moveBackToCart(cartItemId: number): Observable<void> {
    return this.http.put<void>(`${this.baseUrl}/move-back-to-cart/${cartItemId}`, null).pipe(
      tap(() => this.loadCart().subscribe())
    );
  }
}
