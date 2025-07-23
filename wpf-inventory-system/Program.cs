using Microsoft.EntityFrameworkCore;

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
            }

        }


        static void Main(string[] args)
        {

            Product product;
        }
    }
}
