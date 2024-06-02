using EShopping.API.Data;
using EShopping.API.DataTransferObjects.Requests;
using EShopping.API.DataTransferObjects.Resources;
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
            var products = await _appDbContext.Products
                .Include(p => p.ProductCategories)
                .Select(product => new ProductResourceDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Status = product.Status,
                    ProductCategories = product.ProductCategories.Select(productCategory => new ProductCategoryResourceDTO
                    {
                        Id = productCategory.Id,
                        Name = productCategory.Name
                    }).ToList()
                })
                .ToListAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Show(int id)
        {
            var product = await _appDbContext.Products.Include(product => product.ProductCategories).FirstOrDefaultAsync(product => product.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            var productResource = new ProductResourceDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Status = product.Status,
                ProductCategories = product.ProductCategories?.Select(productCategory => new ProductCategoryResourceDTO
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name
                }).ToList()
            };

            return Ok(productResource);
        }

        [HttpPost]
        public async Task<ActionResult> Store(StoreProductRequestDTO request)
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                Status = request.Status,
            };

            await _appDbContext.Products.AddAsync(product);
            await _appDbContext.SaveChangesAsync();

            // Add the selected product categories
            if (request.ProductCategories is not null && request.ProductCategories.Any())
            {
                foreach (int productCategoryId in request.ProductCategories)
                {
                    var productProductCategory = new ProductProductCategory
                    {
                        ProductID = product.Id,
                        ProductCategoryID = productCategoryId
                    };
                    await _appDbContext.ProductProductCategories.AddAsync(productProductCategory);
                }
                await _appDbContext.SaveChangesAsync();
            }

            var getProduct = await _appDbContext.Products.Include(p => p.ProductCategories).FirstOrDefaultAsync(p => p.Id == product.Id);

            var productResource = new ProductResourceDTO
            {
                Id = getProduct.Id,
                Name = getProduct.Name,
                Description = getProduct.Description,
                Price = getProduct.Price,
                Quantity = getProduct.Quantity,
                Status = getProduct.Status,
                ProductCategories = getProduct.ProductCategories?.Select(productCategory => new ProductCategoryResourceDTO
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name
                }).ToList()
            };

            return Ok(productResource);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateProductRequestDTO request)
        {
            var product = await _appDbContext.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.Status = request.Status;

            _appDbContext.Products.Update(product);
            await _appDbContext.SaveChangesAsync();

            // Add the selected product categories
            if (request.ProductCategories is not null && request.ProductCategories.Any())
            {
                product.ProductCategories?.Clear();
                await _appDbContext.SaveChangesAsync();

                foreach (int productCategoryId in request.ProductCategories)
                {
                    var productProductCategory = new ProductProductCategory
                    {
                        ProductID = product.Id,
                        ProductCategoryID = productCategoryId
                    };
                    await _appDbContext.ProductProductCategories.AddAsync(productProductCategory);
                }
                await _appDbContext.SaveChangesAsync();
            }

            var getProduct = await _appDbContext.Products.Include(p => p.ProductCategories).FirstOrDefaultAsync(p => p.Id == product.Id);

            var productResource = new ProductResourceDTO
            {
                Id = getProduct.Id,
                Name = getProduct.Name,
                Description = getProduct.Description,
                Price = getProduct.Price,
                Quantity = getProduct.Quantity,
                Status = getProduct.Status,
                ProductCategories = getProduct.ProductCategories?.Select(productCategory => new ProductCategoryResourceDTO
                {
                    Id = productCategory.Id,
                    Name = productCategory.Name
                }).ToList()
            };

            return Ok(productResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Destroy(int id)
        {
            var product = await _appDbContext.Products.Include(p => p.ProductCategories).FirstOrDefaultAsync(p => p.Id == id);

            if (product is null)
            {
                return NotFound();
            }

            _appDbContext.Products.Remove(product);
            await _appDbContext.SaveChangesAsync();

            return Ok(product);
        }
    }
}
