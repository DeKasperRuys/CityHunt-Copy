using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Models;

namespace WebApplication2.IService
{
    public interface IUserService
    {
        List<Users> GetAllUsers();
        Users SignUpEncryptedUser(Users user);
        Users AuthorizeUser(string username, string password); 

    }
}
