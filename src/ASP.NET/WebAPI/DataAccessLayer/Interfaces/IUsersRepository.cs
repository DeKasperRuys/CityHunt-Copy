using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccessLayer.Interfaces
{
    public interface IUsersRepository
    {
        List<Users> GetAllUsers();
        Users GetUserByEmail(string email);
        Users AddUser(Users user, Highscore highscore);
        bool EmailOrUsernameExists(string email, string username);
        IQueryable<Users> GetUserByEmailAddress(string email);
        //Users SignUpEncryptedUser(Users user);
        Users AuthorizeUserLogin(string username, string password);
        Users UpdateUserByEmail(Users user);
        Users UpdateGPSLocForUser(Users user);
        Users DeleteUser(Users user);
    }
}
