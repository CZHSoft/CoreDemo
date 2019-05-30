using Idp.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Idp.BLL
{
    public interface IUserRepository
    {
        User Find(string username,string password);

        User FindById(string id);

        Claims[] GetClaims(User user);
    }
}
