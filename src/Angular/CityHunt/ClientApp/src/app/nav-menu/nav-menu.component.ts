import { Component } from '@angular/core';
import { SocialLoginModule, AuthService, GoogleLoginProvider } from 'angular-6-social-login';
import { authService } from '../Services/Auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  isExpanded = false;
  User: any;

  constructor(private _socialAuthServ: AuthService, public router: Router, private authService: authService) { }

  collapse() {
    this.isExpanded = false;
  }

  toggle() {
    this.isExpanded = !this.isExpanded;
  }

  signOut(): void {
    this._socialAuthServ.signOut();
    this.User = null;
    this.authService.logout();
    console.log('User has signed out');
  }
}

