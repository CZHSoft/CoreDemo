using Microsoft.EntityFrameworkCore;
using OrderApi.Models;

namespace OrderApi.Dbs
{
    public class OrderContext : DbContext
    {
        public OrderContext(DbContextOptions<OrderContext> options)
           : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
