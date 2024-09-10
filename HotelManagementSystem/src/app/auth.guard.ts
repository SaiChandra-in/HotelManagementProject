import { Injectable } from '@angular/core';
import { CanActivate, Router, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private router: Router) {}

  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot
  ): boolean {
    const role = localStorage.getItem('role');
    const requiredRole = route.data['role'] as string;

    if (role && role === requiredRole) {
      return true;
    } else {
      // Navigate to login if not authorized
      this.router.navigate(['/login']);
      return false;
    }
  }
}
