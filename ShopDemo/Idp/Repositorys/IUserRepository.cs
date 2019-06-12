
using Idp.Models;

namespace Idp.Repositorys
{
    public interface IUserRepository
    {
        User Find(string username, string password);

        User FindById(string id);

        Claims[] GetClaims(User user);
    }
}
