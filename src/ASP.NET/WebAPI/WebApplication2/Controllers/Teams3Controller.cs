using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Teams3Controller : Controller
    {
        private ITeamsService _teamsService;

        public Teams3Controller(ITeamsService teamsService)
        {
            _teamsService = teamsService;
        }
        public List<Team> Get()
        {
            return _teamsService.GetAllTeams();
        }

        [HttpPost]
        public ActionResult<Team> AddTeam([FromBody] Team team)
        {
            _teamsService.PostTeam(team);

            return Created("", team);
        }

        [HttpGet("users/{id}")]
        public List<Users> GetUsersByTeamId(int id)
        {
            return _teamsService.GetUsersByTeamId(id);
        }

        [HttpGet("vragen/{id}")]
        public List<Vragen> GetVragenByTeamId(int id)
        {
            return _teamsService.GetVragenByTeamId(id);
        }

        [HttpGet("ability/{id}")]
        public List<Ability> GetAbilityByTeamId(int id)
        {
            return _teamsService.GetAbilityByTeamId(id);
        }
    }
}