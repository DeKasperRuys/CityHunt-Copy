using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Highscore3Controller : ControllerBase
    {
        private IHighscoreService _highscoreService;
        public Highscore3Controller(IHighscoreService highscoreService)
        {
            _highscoreService = highscoreService;
        }

        // GET: api/Highscore3
        [HttpGet]
        public List<Highscore> GetAllHighscores()
        {
            return _highscoreService.GetAllHighscores();
        }

        // GET: api/Highscore3/users/{int scoreid}
        [HttpGet("users/{id}")]
        public IQueryable<Highscore> GetScoreByUserId(int id)
        {
            return _highscoreService.GetHighscoreByScoreId(id);
        }

        // POST: api/Highscore3/users/{int scoreid}         --> change naar put omdat deze enkel wordt aangepast.
        [HttpPost("users/{id}")]
        public ActionResult<Highscore> PostHighscoreForUser([FromBody] Highscore highscore, int id )
        {
            _highscoreService.PostHighscore(highscore, id);

            return Ok(highscore);
        }

    }
}
