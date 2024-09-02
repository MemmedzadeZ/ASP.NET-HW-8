using Microsoft.EntityFrameworkCore;
using WbDemo.Entities;

namespace WbDemo.Data
{
    public class DemoDbContext : DbContext
    {

        public DemoDbContext(DbContextOptions<DemoDbContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MyDemoDb;Integrated Security=True;");

        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Order> Orders { get; set; }


    }
}
