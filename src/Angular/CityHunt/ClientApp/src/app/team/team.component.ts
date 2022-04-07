import { Component, OnInit } from '@angular/core';
import { authService, ITeam} from '../Services/Auth.service';
import { Router } from '@angular/router';
import { AlertService } from 'ngx-alerts';

@Component({
  selector: 'app-team',
  templateUrl: './team.component.html',
  styleUrls: ['./team.component.css']
})
export class TeamComponent implements OnInit {
  
  teamname: string = "";
  allTeams: ITeam;
  result: object;
  teamId: number = null;
  team = null;
  

  constructor(public router: Router, private authservice: authService,
    private alertService: AlertService
  ) { }

  ngOnInit() {
    this.getTeams()
  }

  submit() {
    var team =
    {
      
      TeamNaam: this.teamname
    }
    console.log(team);
    this.authservice.postTeam(team).subscribe(data => {
      this.result = data;
      console.log(this.result);
      
      this.alertService.success('Team is succesfully added!');
      this.getTeams();

      
    }, err => {
      console.log(err);

    });
    
  }
  //get team list to check if the team already exist.
  getTeams() {
    this.authservice.getTeam().subscribe(data => {
      this.allTeams = data;    


    }, err => {
      console.log(err);
    });
  }
  Search() {
    
  }

  getTeamId(teamID: number, teamNaam: string) {
    this.teamId = teamID;
    this.teamname = teamNaam;
    this.authservice.TeamId = this.teamId;
    console.log(this.teamId);
    this.team = teamNaam;
    if (this.teamId) {
      this.alertService.success('you are now in team ' + this.teamname);
    }
    
  }

  

}
