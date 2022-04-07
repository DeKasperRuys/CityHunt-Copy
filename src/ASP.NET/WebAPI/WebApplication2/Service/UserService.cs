using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Context;
using WebApplication2.IService;
using WebApplication2.Models;

namespace WebApplication2.Service
{
    public class UserService : IUserService
    {
        private DBFirstContext _context;

        public UserService(DBFirstContext context)
        {
            _context = context;
        }

        public List<Users> GetAllUsers()
        {
            //var users = _context.Users
               // .Include(user => user.Vragen);
            return _context.Users.ToList();
        }

        public Users SignUpEncryptedUser(Users user)         // Signup passwoord encrypted
        {
            user.Passwoord = BCrypt.Net.BCrypt.HashPassword(user.Passwoord);
            
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public Users AuthorizeUser(string email, string passwoord)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == email);
            // Decrypt bij aanmelden het gehashte paswoord van de user en check of dit overeenkomt.
            bool isValidPasswd = BCrypt.Net.BCrypt.Verify(passwoord, user.Passwoord);

            if (isValidPasswd) { return user; }
            
            return null;
        }

    }
}
