import { Component, OnInit } from '@angular/core';
import { AlertService } from 'ngx-alerts';
import { authService, IAbility } from '../Services/Auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-ability',
  templateUrl: './ability.component.html',
  styleUrls: ['./ability.component.css']
})
export class AbilityComponent implements OnInit {

  constructor(public router: Router, private authservice: authService,
    private alertService: AlertService) { }

  //abilityType: number = null;
  latitude: string = "";
  longitude: string = "";

  radiobutton: null;
  RadioValue: number;

  QuestData: IAbility;

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
    console.log(this.radiobutton);

  }
  ChosenAbility() {
    if (this.radiobutton == "Bom") {
      return this.RadioValue = 1;
    }
    else if (this.radiobutton == "Skip") {
      return this.RadioValue = 2;
    }
    console.log(this.RadioValue);
  }

  clean() {
    this.latitude = "";
    this.longitude = "";
  }
  submit() {

    this.ChosenAbility();

    var ability =
    {
      AbilityType: this.RadioValue,
      Latitude: this.chosenLat.toString(),
      Longitude: this.chosenLng.toString(),
      IsUsed: false,
      TeamId: this.authservice.TeamId,
      

    }

    //console.log(this.authservice.TeamId);
    console.log(ability);
    this.authservice.postAbility(ability).subscribe(data => {
      console.log(data);
      this.QuestData = data;
      this.alertService.success('Question succesfully send!');

    }, err => {
      console.log(err);

    });
    this.clean();
  }

}


