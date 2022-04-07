using DataAccessLayer.Interfaces;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Implementations
{
    class TeamsRepository : ITeamsRepository
    {
        AppDBContext _context;

        public TeamsRepository(AppDBContext context)
        {
            _context = context;
        }
        public List<Team> GetAllTeams()
        {
            return _context.Team.ToList();
        }

        public bool TeamExists(string teamNaam)
        {
            var teamToCheck = _context.Team.Any(t => t.TeamNaam == teamNaam);

            if (teamToCheck) return true;

            return false;
        }

        public Team PostTeam(Team team)
        {
            _context.Team.Add(team);
            _context.SaveChanges();

            return team;
        }
        // volgende 3 methodes kunnen ingekort worden door 1 methode te maken...
        public List<Users> GetUsersByTeamId(int teamId)
        {
            IQueryable<Users> u = _context.Users;
            u = u.Where(d => d.TeamId == teamId);

            return u.ToList();
        }
        public List<Vragen> GetVragenByTeamId(int teamId)
        {
            IQueryable<Vragen> u = _context.Vragen;
            u = u.Where(d => d.TeamId == teamId);

            return u.ToList();
        }

        public List<Ability> GetAbilityByTeamId(int teamId)
        {
            IQueryable<Ability> u = _context.Ability;
            u = u.Where(d => d.TeamId == teamId);

            return u.ToList();
        }
    }
}
