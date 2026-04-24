import { inject, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AIChatService {
  private baseUrl = `${environment.apiUrl}/api/AIChat`;
  private http = inject(HttpClient);

  sendMessage(payload: any): Observable<any> {
    return this.http.post<any>(`${this.baseUrl}`, payload);
  }
}