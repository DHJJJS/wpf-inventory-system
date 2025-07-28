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
                base.OnConfiguring(optionsBuilder);
                optionsBuilder.UseMySql("Server=localhost;Database=inventory;Uid=root;Pwd=root;",
                                         ServerVersion.AutoDetect("Server=localhost;Database=inventory;Uid=root;Pwd=root;")
                                         );
            }

        }


        static void Main(string[] args)
        {

            Product product;
        }
    }
}
