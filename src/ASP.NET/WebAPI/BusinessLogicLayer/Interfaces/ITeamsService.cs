using Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface ITeamsService
    {
        List<Team> GetAllTeams();
        List<Users> GetUsersByTeamId(int teamId);
        List<Vragen> GetVragenByTeamId(int teamId);
        List<Ability> GetAbilityByTeamId(int teamId);
        Team PostTeam(Team team);
    }
}
