
using Idp.Dbs;
using Idp.Models;
using System.Linq;

namespace Idp.Repositorys
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
