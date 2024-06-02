using EShopping.API.Data;
using EShopping.API.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EShopping.API.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public ProductController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public async Task<ActionResult> Index()
        {
            IQueryable<Product> productsQuery = _appDbContext.Products.Include(p => p.ProductCategories);

            List<Product> products = await productsQuery.ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Show(int id)
        {
            var product = await _appDbContext.Products.Include(p => p.ProductCategories).FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }
    }
}
