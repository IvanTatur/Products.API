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
    }
}