using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Products.API.Repositories
{
    public class ProductRepository
    {
        private readonly ProductContext db;
        private readonly ILogger<ProductRepository> logger;

        public ProductRepository(ProductContext context, ILogger<ProductRepository> logger)
        {
            db = context;
            this.logger = logger;
        }

        public async Task<IEnumerable<Product>> GetAll()
        {
            logger.LogInformation("called ProductRepository GetAll()");
            return await db.Product.ToListAsync();
        }

        public async Task<Product> Get(int id)
        {
            logger.LogInformation($"called ProductRepository Get() with id = {id}");

            var product = await db.Product.FirstOrDefaultAsync(model => model.ProductId == id);
            return product;
        }

        public async Task<Product> Add(Product product)
        {
            logger.LogInformation($"called ProductRepository Add(). ProductId = {product.ProductId}");
            db.Product.Add(product);
            await db.SaveChangesAsync();

            return product;
        }

        public async Task<Product> Update(Product product)
        {
            logger.LogInformation($"called ProductRepository Update(). ProductId = {product.ProductId}");
            db.Update(product);
            await db.SaveChangesAsync();

            return product;
        }

        public async Task Delete(int id)
        {
            logger.LogInformation("called ProductRepository Delete() for ProductId = id}");

            var product = db.Product.FirstOrDefault(model => model.ProductId == id);
            if (product == null)
            {
                logger.LogInformation("called ProductRepository Delete(). Entered id not found.}");
                return;
            }

            db.Product.Remove(product);
            await db.SaveChangesAsync();
        }
    }
}