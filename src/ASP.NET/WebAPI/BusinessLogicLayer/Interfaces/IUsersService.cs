using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusinessLogicLayer.Interfaces
{
    public interface IUsersService
    {
        List<Users> GetAllUsers();
        //Users GetUserByEmail(string email);
        IQueryable<Users> GetUserByEmailAddress(string email);
        Users AddUser(Users user, Highscore highscore);
        Users SignUpEncryptedUser(Users user);
        Users AuthorizeUserLogin(string username, string password);
        Users UpdateUserByEmail(Users user);
        Users UpdateGPSLocForUser(Users user);
        Users DeleteUser(Users user);
    }
}
