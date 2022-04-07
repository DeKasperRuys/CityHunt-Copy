import { Component } from '@angular/core';
import { authService } from '../Services/Auth.service';
import { Router } from '@angular/router';
import { SocialLoginModule, AuthService, GoogleLoginProvider, FacebookLoginProvider } from 'angular-6-social-login';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent {


  //user: any;


  constructor(private _socialAuthServ: AuthService, public router: Router, private authService: authService) { }


  signOut(): void {
    this._socialAuthServ.signOut();
    this.authService.logout();
    console.log('User has signed out');
  }

}
