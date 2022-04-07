using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface ITeamsRepository
    {
        List<Team> GetAllTeams();

        List<Users> GetUsersByTeamId(int teamId);

        List<Vragen> GetVragenByTeamId(int teamId);

        List<Ability> GetAbilityByTeamId(int teamId);

        bool TeamExists(string teamNaam);

        Team PostTeam(Team team);


    }
}
