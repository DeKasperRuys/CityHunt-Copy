using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLogicLayer.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Service;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Users3Controller : Controller
    {
        private IUsersService _usersService;
        private IJwtAuthManager _jwtAuthManager;
        public Users3Controller(IUsersService usersService, IJwtAuthManager jwtAuthManager)
        {
            _usersService = usersService;
            _jwtAuthManager = jwtAuthManager;
        }

        [HttpGet]
        public List<Users> GetAllUsers()
        {
            return _usersService.GetAllUsers();
        }

        [HttpPost("email")]
        public IQueryable<Users> GetUserByEmail([FromBody] string email)
        {
            return _usersService.GetUserByEmailAddress(email);
        }

        [HttpPost]
        public ActionResult<Users> SignUp([FromBody] Users user)
        {
            _usersService.SignUpEncryptedUser(user);

            return Created("", user);
        }

        [HttpPost("login")]
        public ActionResult<Users> Login([FromBody] Users user)
        {
            var result = _usersService.AuthorizeUserLogin(user.Email, user.Passwoord);

            if (result == null) return Unauthorized();

            var JWT = _jwtAuthManager.GenerateJWTForAuthorizedUser(user.Email, user.Passwoord);

            return Ok(JWT);
        }

        [HttpDelete]
        public ActionResult<Users> DeleteUser([FromBody]Users user)
        {
            return Ok(_usersService.DeleteUser(user));
        }

        [HttpPut("updateuser")]
        public ActionResult<Users> UpdateUserByEmail([FromBody] Users user)
        {
            return Ok(_usersService.UpdateUserByEmail(user));
        }

        [HttpPut("updateloc")]
        public ActionResult<Users> UpdateGPSLocation([FromBody] Users user)
        {
            return Ok(_usersService.UpdateGPSLocForUser(user));
        }
    }
}