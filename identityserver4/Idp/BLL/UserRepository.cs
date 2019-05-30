using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Idp.Db;

namespace Idp.BLL
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext _context;

        public UserRepository(UserContext context)
        {
            _context = context;
        }

        public User Find(string username, string password)
        {
            return _context.Users.FirstOrDefault(o => o.UserName == username && o.Password == password);
        }

        public User FindById(string id)
        {
            return _context.Users.FirstOrDefault(o => o.UserId == id);
        }

        public Claims[] GetClaims(User user)
        {
            return _context.UserClaims.Where(o => o.User == user).ToArray();
        }
    }
}
