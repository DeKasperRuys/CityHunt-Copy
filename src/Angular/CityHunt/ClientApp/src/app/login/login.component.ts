import { Component, OnInit, Injectable } from '@angular/core';
import { SocialLoginModule, AuthService, GoogleLoginProvider, FacebookLoginProvider } from 'angular-6-social-login';
//import { Redirect } from 'auth0-js';
import { Router } from '@angular/router';
import { NgForm } from '@angular/forms';
import { authService, IUser } from '../Services/Auth.service';
import { AlertService } from 'ngx-alerts';




@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
  providers: [SocialLoginModule, AuthService]
})

@Injectable({
  providedIn: 'root'
})

export class LoginComponent implements OnInit {

  user: any;



  errorMessage: string = "";

  result: JsonWebKey;

  userData: IUser;

  email: string = "";
  password: string = "";

  constructor(
                private alertService: AlertService,
                private _socialAuthServ: AuthService,
                public router: Router,
                private authservice: authService
                ) { }

  ngOnInit() {
    

  }
 

  login(){
    var user = 
    { 
      Email: this.email,
      Passwoord: this.password
    }
    //console.log(user);
    
    this.authservice.postLogin(user).subscribe( data => {
      //console.log(data);
      this.userData = data;
     
    }, err => {
      console.log(err);
        this.result = err.error.text;
        localStorage.setItem('idToken', this.result.toString());
        this.alertService.success('User logged in!');
        this.router.navigate(['/home']);
    });
    
  }


  socialSignIn(platform: string): void {
    if (platform === 'google') {
      var platforms = GoogleLoginProvider.PROVIDER_ID;
     
    }
    else if (platform === 'facebook') {
      var platforms = FacebookLoginProvider.PROVIDER_ID;
     
    }
    this._socialAuthServ.signIn(platforms).then((Response) => {
      console.log(platform + " logged in user data is =", Response);

      this.user = Response;
      if (platform === 'google') {
        localStorage.setItem('idToken', this.user.idToken);
        this.alertService.success('User logged in!');
        this.router.navigate(['/home']);
      }
      else if (platform === 'facebook') {
        localStorage.setItem('idToken', this.user.token);
        this.alertService.success('User logged in!');
        this.router.navigate(['/home']);

      }

    }

    );

  }
 


  signOut(): void {
    this._socialAuthServ.signOut();
    this.user = null;
    this.authservice.logout();
    this.router.navigate(['/']);
    //console.log('User has signed out');
  }
}

