import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { JwtHelperService } from "@auth0/angular-jwt";
import { authService } from '../Services/Auth.service';

@Injectable({ providedIn: 'root' })
export class AuthGuard implements CanActivate {

  helper = new JwtHelperService;

  constructor(
    private router: Router,
    private authService: authService
  ) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {

    const token = localStorage.getItem('idToken');

    if (!this.helper.isTokenExpired(token)) {
      // logged in so return true
      return true;
    }
    else {
    // not logged in so redirect to login page with the return url
      this.router.navigate(['/login'], { queryParams: { returnUrl: state.url } });
      return false;
    }

    
  }


}
