import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class PaymentApiService {
  private http = inject(HttpClient);
  
  private readonly baseUrl = `${environment.apiUrl}/api/Payment`;

  createCheckoutSession(orderId: number): Observable<{ sessionId: string; url: string }> {
    
    return this.http.post<{ sessionId: string; url: string }>(
      `${this.baseUrl}/create-checkout-session`, 
      orderId
    );
  }
}