using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Interfaces;
using Domain;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementations
{
    public class TeamsService : ITeamsService
    {
        private ITeamsRepository _teamsRepository;

        public TeamsService(ITeamsRepository teamsRepository)
        {
            _teamsRepository = teamsRepository;
        }

        public List<Ability> GetAbilityByTeamId(int teamId)
        {
            return _teamsRepository.GetAbilityByTeamId(teamId);
        }

        public List<Team> GetAllTeams()
        {
            return _teamsRepository.GetAllTeams();
        }

        public List<Users> GetUsersByTeamId(int teamId)
        {
            return _teamsRepository.GetUsersByTeamId(teamId);
        }

        public List<Vragen> GetVragenByTeamId(int teamId)
        {
            return _teamsRepository.GetVragenByTeamId(teamId);
        }

        public Team PostTeam(Team team)
        {
            if (team.TeamNaam.Length > 15) throw new ArgumentOutOfRangeException("Teamnaam is te lang.");

            if (_teamsRepository.TeamExists(team.TeamNaam) != true) return _teamsRepository.PostTeam(team);
            else throw new ArgumentException("Team bestaat al");


        }
    }
}
