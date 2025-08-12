using AbySalto.Mid.WebApi.Models;
using AbySalto.Mid.WebApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace AbySalto.Mid.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : Controller
    {       

        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetProducts()
        {
            ProductService productService = new ProductService(new MemoryCache(new MemoryCacheOptions()), new HttpClient());
            ProductResponse productResponse = await productService.GetProductsAsync();
            return Ok(productResponse.Products);
        }

        [HttpGet("get-product")]
        public async Task<IActionResult> GetProduct(int productId)
        {
            ProductService productService = new ProductService(new MemoryCache(new MemoryCacheOptions()), new HttpClient());
            Product product = await productService.GetProductAsync(productId);
            return Ok(product);
        }

        [HttpGet("get-products-paging")]
        public async Task<IActionResult> GetProductsPaging(int page, int pageSize = 10, string sortBy = "", string sortOrder = "")
        {
            ProductService productService = new ProductService(new MemoryCache(new MemoryCacheOptions()), new HttpClient());
            ProductResponse productResponse = await productService.GetProductsAsync(page, pageSize, sortBy);
            return Ok(productResponse.Products);
        }


    }
}

