import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { authService, IRegister } from '../Services/Auth.service';
import { Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-sign-up',
  templateUrl: './sign-up.component.html',
  styleUrls: ['./sign-up.component.css']
})
export class SignUpComponent implements OnInit {

  userData: IRegister;

  errorMessage: string = "";

  username: string = "";
  name: string = "";
  lastname: string = "";
  email: string = "";
  password: string = "";
  confirm_password: string = "";


  constructor(private authservice: authService, public router: Router,
    private alertService: AlertService
  ) { }

  ngOnInit() {
  }

  SignUp() {
    var user =
    {
      naam: this.name,
      achternaam: this.lastname,
      email: this.email,
      username: this.username,
      passwoord: this.password,
      teamId: 1
    }
    console.log(user);

    if (this.password === this.confirm_password) {
      this.authservice.PostRegister(user).subscribe(data => {
        console.log(data);
        this.userData = data;

      }, err => {
          console.log(err);
          this.alertService.success('User sign in!');
      });
      this.router.navigate(['/login']);
    }
    else {
      this.errorMessage = "password and confirm password does not match!";
    }

    

  }

}
