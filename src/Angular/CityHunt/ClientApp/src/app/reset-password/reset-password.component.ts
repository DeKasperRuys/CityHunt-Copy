import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';

@Component({
  selector: 'app-reset-password',
  templateUrl: './reset-password.component.html',
  styleUrls: ['./reset-password.component.css']
})
export class ResetPasswordComponent implements OnInit {

  email: string = "";
 

  constructor() { }

  ngOnInit() {
  }



  

  submit() {
    var reset =
    {
      email: this.email
    }
    console.log(reset);
  }

}
