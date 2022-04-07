using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Context;
using WebApplication2.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication2.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VragenController : ControllerBase
    {
        DBFirstContext _context { get; set; }

        public VragenController(DBFirstContext ctxt)
        {
            _context = ctxt;
        }

        //[Authorize]
        // GET: api/Vragen
        [HttpGet]
        public List<Vragen> GetVragen()
        {
            return _context.Vragen.ToList();
        }
        /*
        // GET: api/Vragen/5
        [HttpGet("{id}")]
        public List<Vragen> GetVragenByUserId(int id)
        {
            IQueryable < Vragen > u  = _context.Vragen;
            u = u.Where(d => d.UserId == id);
            return u.ToList();
        }
        */

        // POST: api/Vragen
        [HttpPost]
        public ActionResult<Vragen> AddVraag([FromBody] Vragen vraag)
        {
            if (!ModelState.IsValid) // controlleer de velden op de juiste inhoud, zie models
                return BadRequest(ModelState);

            _context.Vragen.Add(vraag);

            _context.SaveChanges();

            return Created("", vraag);

        }
        /*
        // PUT: api/Vragen/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        */
    }
}
