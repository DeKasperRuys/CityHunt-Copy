import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { authService, IQuestion } from '../Services/Auth.service';
import { AlertService } from 'ngx-alerts';


@Component({
  selector: 'app-questions',
  templateUrl: './questions.component.html',
  styleUrls: ['./questions.component.css']
})
export class QuestionsComponent implements OnInit {

  QuestData: IQuestion;

  teamname: string = "";
  Q: string = "";
  A1: string = "";
  A2: string = "";
  A3: string = "";
  A4: string = "";
  latitude: string = "";
  longitude: string = "";
  hint: string = "";
  points: number;
  teamId: number = 1;

  constructor(public router: Router, private authservice: authService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    
  }

  

  lat = 51.2194475;
  lng = 4.4024643;
  chosenLat = null;
  chosenLng = null;

  mapType = 'satellite';

  addMarker(event) {
    this.chosenLat = event.coords.lat;
    this.chosenLng = event.coords.lng;
    console.log(this.lat);
    console.log(this.lng);
    
  }

  clean() {
    this.teamname = "";
    this.Q = "";
    this.A1 = "";
    this.A2 = "";
    this.A3 = "";
    this.A4 = "";
    this.latitude = "";
    this.longitude = "";
    this.hint = "";
    this.points = null; 

  }

  submit() {
    var Question =
    {
      
      Title: this.Q,
      JuisteAntwoord: this.A1,
      Antwoord1: this.A2,
      Antwoord2: this.A3,
      Antwoord3: this.A4,
      Lat: this.chosenLat.toString(),
      Long: this.chosenLng.toString(),
      Hint: this.hint,
      Points: Number(this.points),
      TeamId: this.authservice.TeamId,
     
    }
    console.log(this.authservice.TeamId);
    console.log(Question);
    this.authservice.postQuestion(Question).subscribe(data => {
      console.log(data);
      this.QuestData = data;
      this.alertService.success('Question succesfully send!');
      
    }, err => {
      console.log(err);
      
    });
    this.clean();
  }
  


}
