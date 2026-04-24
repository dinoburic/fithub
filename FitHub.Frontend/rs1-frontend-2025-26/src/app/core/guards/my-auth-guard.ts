// src/app/core/guards/auth.guard.ts
import { inject } from '@angular/core';
import { CanActivateFn, ActivatedRouteSnapshot, Router } from '@angular/router';
import { CurrentUserService } from '../services/auth/current-user.service';

export const myAuthGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
  const currentUser = inject(CurrentUserService);
  const router = inject(Router);

  const authData = route.data['auth'] as MyAuthRouteData | undefined;
  const requireAuth = authData ? authData.requireAuth === true : true;
  
  const requireAdmin = authData?.requireAdmin === true;
  const requireManager = authData?.requireManager === true;
  const requireEmployee = authData?.requireEmployee === true;

  // FIXED: Proper signal invocation with ()
  const isAuth = currentUser.isAuthenticated();

  if (requireAuth && !isAuth) {
    router.navigate(['/auth/login']);
    return false;
  }

  if (!requireAuth) {
    return true;
  }

  // FIXED: Removed snapshot. Read directly from computed signal
  // uz obavezno pozivanje sa () kako bismo dobili boolean vrijednost
  if (requireAdmin && !currentUser.isAdmin()) {
    router.navigate([currentUser.getDefaultRoute()]);
    return false;
  }

  if (requireManager && !currentUser.isManager()) {
    router.navigate([currentUser.getDefaultRoute()]);
    return false;
  }

  if (requireEmployee && !currentUser.isEmployee()) {
    router.navigate([currentUser.getDefaultRoute()]);
    return false;
  }

  return true;
};

export interface MyAuthRouteData {
  requireAuth?: boolean;
  requireAdmin?: boolean;
  requireManager?: boolean;
  requireEmployee?: boolean;
}

export function myAuthData(data: MyAuthRouteData): { auth: MyAuthRouteData } {
  return { auth: data };
}
