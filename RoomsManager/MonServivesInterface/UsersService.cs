using Microsoft.IdentityModel.Tokens;
using RoomsManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RoomsManager.MonServives
{
    interface UsersService
    {
        Users login(string username, string password);
        Boolean dateValid(DateTime dt1, DateTime dt2);
        SigningCredentials generateCertificat(String key);
        SecurityToken createToken(Users user);
        Boolean tokenValidate(String token);
    }
}
