using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Context;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        DBFirstContext _context { get; set; }

        public TeamController(DBFirstContext ctxt)
        {
            _context = ctxt;
        }
        // GET: api/Team
        [HttpGet]
        public List<Team> Get()
        {
            return _context.Team.ToList();
        }

        // GET: api/team/users/5
        [HttpGet("users/{id}")]
        public List<Users> GetUsersByTeamId(int id)
        {
            IQueryable < Users > u  = _context.Users;
            u = u.Where(d => d.TeamId == id);

            return u.ToList();
        }

        // GET: api/team/vragen/5
        [HttpGet("vragen/{id}")]
        public List<Vragen> GetVragenByTeamId(int id)
        {
            IQueryable<Vragen> u = _context.Vragen;
            u = u.Where(d => d.TeamId == id);

            return u.ToList();
        }

        // POST: api/Team
        [HttpPost]
        public ActionResult<Team> AddTeam([FromBody] Team team)
        {
            if (!ModelState.IsValid) // controlleer de velden op de juiste inhoud, zie models
                return BadRequest(ModelState);
            _context.Team.Add(team);
            _context.SaveChanges();

            return Created("", team);
        }

        // PUT: api/Team/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
