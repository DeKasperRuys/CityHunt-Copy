using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Context;

namespace WebApplication2.Service
{
    public interface IJwtAuthManager
    {
        public string GenerateJWTForAuthorizedUser(string username, string password);
    }
}
