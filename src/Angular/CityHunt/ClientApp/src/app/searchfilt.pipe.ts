import { Pipe, PipeTransform } from '@angular/core';
import { ITeam } from './Services/Auth.service';

@Pipe({
  name: 'searchfilt',
  pure: true
})
export class SearchfiltPipe implements PipeTransform {

  transform(team: ITeam[], teamname: string): ITeam[] {
    if (!team || !teamname) {
      return team;
    }
    
    return team.filter(team => team.teamNaam.toLowerCase().indexOf(teamname.toLowerCase()) !== -1);
  }

}
