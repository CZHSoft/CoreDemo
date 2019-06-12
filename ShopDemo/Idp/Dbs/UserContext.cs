using Idp.Models;
using Microsoft.EntityFrameworkCore;


namespace Idp.Dbs
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options)
           : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Claims> UserClaims { get; set; }
    }

    

    
}
