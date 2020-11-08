using Microsoft.EntityFrameworkCore;

namespace Products.API.Repositories
{
    public class ProductContext : DbContext
    {
        public ProductContext()
        {
        }

        public ProductContext(DbContextOptions<ProductContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Product { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    "Server=sql-tatur.eastus.cloudapp.azure.com;Database=AdventureWorks2017;User ID=sa; Password=Adminpassword1;");
        }
    }
}