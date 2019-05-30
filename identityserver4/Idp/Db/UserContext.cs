using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Idp.Db
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

    public class User
    {
        [Key]
        [MaxLength(32)]
        public string UserId { get; set; }

        [MaxLength(32)]
        public string UserName { get; set; }

        [MaxLength(50)]
        public string Password { get; set; }

        public bool IsActive { get; set; }//是否可用

        public virtual ICollection<Claims> Claims { get; set; }

    }

    public class Claims
    {
        [MaxLength(32)]
        public int ClaimsId { get; set; }

        [MaxLength(32)]
        public string Type { get; set; }

        [MaxLength(32)]
        public string Value { get; set; }

        public virtual User User { get; set; }

    }
}
