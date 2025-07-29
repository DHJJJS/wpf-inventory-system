using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace wpf_inventory_system
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
            optionsBuilder.UseSqlServer(@"Server=(LocalDB)\MSSQLLocalDB;Database=inventory;Trusted_Connection=true;");
        }

    }

    public class Program
    {

        static void Main(string[] args)
        {

            //            Product product;

            using var context = new ApplicationDbContext();
            var product = new Product
            {
                ProductInventory = 1,
                ProductName = "라",
                ProductPrice = 1,
            };
            context.Products.Add(product);
            context.SaveChanges();

            var products = context.Products.ToList();
            foreach (var p in products)
            {
                Console.WriteLine($"상품 : {p.ProductName}, 가격 : {p.ProductPrice}");
            }
        }
    }
}
