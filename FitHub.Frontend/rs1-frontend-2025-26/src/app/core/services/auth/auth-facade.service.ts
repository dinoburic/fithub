// src/app/core/services/auth/auth-facade.service.ts
import { Injectable, inject, signal, computed } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, of, tap, catchError, map } from 'rxjs';
import { jwtDecode } from 'jwt-decode';

import { AuthApiService } from '../../../api-services/auth/auth-api.service';
import {
  ExternalLoginCommand,
  LoginCommand,
  LoginCommandDto,
  LogoutCommand,
  RefreshTokenCommand,
  RefreshTokenCommandDto,
  RegisterCommand,
} from '../../../api-services/auth/auth-api.model';

import { AuthStorageService } from './auth-storage.service';
import { CurrentUserDto } from './current-user.dto';
import { JwtPayloadDto } from './jwt-payload.dto';

/**
 * Glavni auth servis (façade).
 * - talks to AuthApiService (HTTP)
 * - talks to AuthStorageService (localStorage)
 * - decodes JWT and keeps CurrentUser as a signal
 *
 * Koristi se u:
 * - interceptoru (getAccessToken, refresh)
 * - guardovima (isAuthenticated, isAdmin)
 * - komponentama (login, logout, navbar)
 */
@Injectable({ providedIn: 'root' })
export class AuthFacadeService {
  private api = inject(AuthApiService);
  private storage = inject(AuthStorageService);
  private router = inject(Router);


  // === REACTIVE STATE: current user ===

  private _currentUser = signal<CurrentUserDto | null>(null);

  /** readonly signal for UI - read as auth.currentUser() */
  currentUser = this._currentUser.asReadonly();

  /** computed signali nad current userom */
  isAuthenticated = computed(() => !!this._currentUser());
  isAdmin = computed(() => this._currentUser()?.isAdmin ?? false);
  isManager = computed(() => this._currentUser()?.isManager ?? false);
  isEmployee = computed(() => this._currentUser()?.isEmployee ?? false);

  constructor() {
    // try initialization from existing access token
    this.initializeFromToken();
  }

  // =========================================================
  // PUBLIC API
  // =========================================================

  /**
   * Login korisnika (email + password).
   * Snima tokene u storage, dekodira JWT i popunjava current user state.
   */
  login(payload: LoginCommand): Observable<void> {
    return this.api.login(payload).pipe(
      tap((response: LoginCommandDto) => {
        this.storage.saveLogin(response);           // access + refresh + expiries
        this.decodeAndSetUser(response.accessToken); // popuni _currentUser
        this.api.setLoggedIn(true);
      }),
      map(() => void 0)
    );
  }

  externalLogin(payload: ExternalLoginCommand): Observable<void> {
    return this.api.externalLogin(payload).pipe(
      tap((response: LoginCommandDto) => {
        this.storage.saveLogin(response);          // access + refresh
        this.decodeAndSetUser(response.accessToken); // popuni _currentUser signal
        this.api.setLoggedIn(true);
      }),
      map(() => void 0)
    );
  }

  register(payload: RegisterCommand) {
    return this.api.register(payload);
  }

  /**
   * User logout:
   * - clear local state and tokens
   * - attempt to invalidate refresh token on server (no drama on error)
   */
  logout(): Observable<void> {
    const refreshToken = this.storage.getRefreshToken();

    // 1) clear locally (optimistic logout)
    this.clearUserState();

    // 2) no refresh token → no API call
    if (!refreshToken) {
      return of(void 0);
    }

    const payload: LogoutCommand = { refreshToken };

    // 3) attempt server-side logout, ignore errors
    this.api.setLoggedIn(false);
    return this.api.logout(payload).pipe(catchError(() => of(void 0)));
  }

  /**
   * Refresh access token – uses refresh token.
   * Calls interceptor when 401 is received.
   */
  refresh(payload: RefreshTokenCommand): Observable<RefreshTokenCommandDto> {
    return this.api.refresh(payload).pipe(
      tap((response: RefreshTokenCommandDto) => {
        this.storage.saveRefresh(response);           // snimi nove tokene
        this.decodeAndSetUser(response.accessToken);  // update current usera
      })
    );
  }

  /**
   * Utility for guards/interceptors - clear auth state and redirect to /login.
   */
  redirectToLogin(): void {
    this.clearUserState();
    this.router.navigate(['auth/login']);
  }

  // =========================================================
  // GETTERI ZA INTERCEPTOR
  // =========================================================

  /**
   * Access token za Authorization header.
   */
  getAccessToken(): string | null {
    return this.storage.getAccessToken();
  }

  /**
   * Refresh token za refresh poziv.
   */
  getRefreshToken(): string | null {
    return this.storage.getRefreshToken();
  }

  // =========================================================
  // PRIVATE HELPERS
  // =========================================================

  /**
   * On app start (constructor) - try to restore state from existing token.
   */
  private initializeFromToken(): void {
    const token = this.storage.getAccessToken();
    if (token) {
      this.decodeAndSetUser(token);
    }
  }

  /**
   * Dekodiraj JWT i postavi current user state.
   */
  private decodeAndSetUser(token: string): void {
    try {
      const payload = jwtDecode<JwtPayloadDto>(token);

      const user: CurrentUserDto = {
        userId: Number(payload.sub),
        email: payload.email,
        isAdmin: payload.is_admin === 'true',
        isManager: payload.is_manager === 'true',
        isEmployee: payload.is_employee === 'true',
        tokenVersion: Number(payload.ver),
      };

      this._currentUser.set(user);
    } catch (error) {
      console.error('Failed to decode JWT token:', error);
      this._currentUser.set(null);
    }
  }

  /**
   * Clear user state and all tokens from storage.
   */
  private clearUserState(): void {
    this._currentUser.set(null);
    this.storage.clear();
  }
}
