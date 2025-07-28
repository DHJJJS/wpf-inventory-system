using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql;

namespace wpf_inventory_system
{
    internal class Program
    {
        public class Product
        {
            public int ProductId { get; set; }

            public string ProductName { get; set; }

            public int ProductPrice { get; set; }

            public int ProductInventory { get; set; }

        }

        public class ApplicationDbContext : DbContext
        {
            public DbSet<Product> Products { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {

//                optionsBuilder.UseSqlServer("YourConnectionString");
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseSqlServer("Server=localhost;Database=inventory;Trusted_Connection=true;");
            }

        }


        static void Main(string[] args)
        {

            Product product;
        }
    }
}
