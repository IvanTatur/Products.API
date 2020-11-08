using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Products.API.Repositories;

namespace Products.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductRepository repository;

        public ProductsController(ProductRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            return await repository.GetAll();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> Get(int id)
        {
            var product = await repository.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            return new ObjectResult(product);
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Post(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            await repository.Add(product);

            return Ok(product);
        }

        [HttpPut]
        public async Task<ActionResult<Product>> Put(Product product)
        {
            if (product == null)
            {
                return BadRequest();
            }

            if (await repository.Get(product.ProductId) == null)
            {
                return NotFound();
            }

            await repository.Update(product);
            return Ok(product);
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await repository.Get(id);
            if (product == null)
            {
                return NotFound();
            }

            await repository.Delete(id);
            return Ok(product);
        }
    }
}