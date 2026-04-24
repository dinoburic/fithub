import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import {
  LoginCommand,
  LoginCommandDto,
  RefreshTokenCommand,
  RefreshTokenCommandDto,
  LogoutCommand,
  RegisterCommand,
  ExternalLoginCommand
} from './auth-api.model';

@Injectable({
  providedIn: 'root'
})
export class AuthApiService {
  private readonly baseUrl = `${environment.apiUrl}/api/Auth`;
  private http = inject(HttpClient);
  private _isLoggedIn = new BehaviorSubject<boolean>(false);
  public isLoggedIn$ = this._isLoggedIn.asObservable();

  /**
   * POST /Auth/login
   * Authenticate user and get access/refresh tokens.
   */
  login(payload: LoginCommand): Observable<LoginCommandDto> {
    return this.http.post<LoginCommandDto>(`${this.baseUrl}/login`, payload);
    
  }

  /**
   * POST /Auth/refresh
   * Refresh access token using refresh token.
   */
  refresh(payload: RefreshTokenCommand): Observable<RefreshTokenCommandDto> {
    return this.http.post<RefreshTokenCommandDto>(`${this.baseUrl}/refresh`, payload);
  }

  /**
   * POST /Auth/logout
   * Invalidate refresh token and logout user.
   */
  logout(payload: LogoutCommand): Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/logout`, payload);
  }

  register(command: RegisterCommand):Observable<void> {
    return this.http.post<void>(`${this.baseUrl}/registration`, command);
  }

    setLoggedIn(value: boolean) {
    this._isLoggedIn.next(value);
  }

  isLoggedIn(): boolean {
    return this._isLoggedIn.value;
  }

 externalLogin(payload: ExternalLoginCommand): Observable<LoginCommandDto> {
    return this.http.post<LoginCommandDto>(`${this.baseUrl}/external-login`, payload);
  }

}
