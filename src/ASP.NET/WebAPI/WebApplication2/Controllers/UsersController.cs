using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.IdentityModel.Tokens;
using WebApplication2.Context;
using WebApplication2.IService;
using WebApplication2.Models;
using WebApplication2.Service;
        
namespace WebApplication2.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        UserService _userService;
        IJwtAuthManager _jwtAuthManager;
        DBFirstContext _context { get; set; }

        public UsersController(DBFirstContext context, UserService userService, IJwtAuthManager jwtAuthManager)
        {
            _context = context;
            _userService = userService;
            _jwtAuthManager = jwtAuthManager;
        }

        // GET: api/Users
        [AllowAnonymous]
        [HttpGet]
        public List<Users> GetAllUsers()
        {
            //var users = _context.Users.Include(c => c.Vragen).ToList();//.Where(c => c.UserId == 0).ToList();
            //IQueryable < Users > u  = _context.Users;
            //u = u.Where(d => d.UserId);

            return _userService.GetAllUsers();
        }

        // POST: api/Users
        [AllowAnonymous]
        [HttpPost]
        public ActionResult<Users> SignUp([FromBody] Users user)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);  // controlleer de velden op de juiste inhoud, zie models

            if (_context.Users.Any(u => u.Email == user.Email)) return BadRequest("User Already Exists"); //Controlleer of de user(name) al bestaat

            _userService.SignUpEncryptedUser(user);

            return Created("", user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public ActionResult<Users> Login([FromBody] Users user)
        {
            var result = _userService.AuthorizeUser(user.Email, user.Passwoord);

            if (result == null) return Unauthorized();

            var JWT = _jwtAuthManager.GenerateJWTForAuthorizedUser(user.Email, user.Passwoord);

            return Ok(JWT);
        }

        [AllowAnonymous]
        [HttpPut("updateuser")]
        public ActionResult<Users> UpdateUserByEmail([FromBody] Users user)
        {
            var userExists = _context.Users.SingleOrDefault(x => x.Email == user.Email);

            if (userExists == null) { return NotFound(); }

            userExists.Naam = user.Naam;
            userExists.Achternaam = user.Achternaam;
            userExists.Email = user.Email;
            userExists.Passwoord = BCrypt.Net.BCrypt.HashPassword(user.Passwoord); 
            userExists.Username = user.Username;
            userExists.TeamId = user.TeamId;              // Team id moet worden meegegeven ( maak groepid 99999 en teamnaam 'geen team' )

            _context.Users.Update(userExists);
            _context.SaveChanges();

            return Ok(userExists);
        }

        [AllowAnonymous]
        [HttpPut("updateloc")]
        public ActionResult<Users> UpdateGPSLocation([FromBody] Users user)
        {
            var userExists = _context.Users.SingleOrDefault(x => x.Email == user.Email);

            if (userExists == null) { return NotFound(); }

            userExists.Lat = user.Lat;        //Update enkel lat/long velden
            userExists.Long = user.Long;

            _context.Users.Update(userExists);
            _context.SaveChanges();

            return Ok(userExists);
        }

        [AllowAnonymous]
        [HttpDelete]
        public ActionResult<Users> DeleteUser([FromBody] Users user)
        {
            var userExists = _context.Users.SingleOrDefault(x => x.UserId == user.UserId);

            if (userExists == null) { return NotFound(); }

            _context.Users.Remove(userExists);
            _context.SaveChanges();

            return Ok(userExists);
        }

    }
}
