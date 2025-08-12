using AbySalto.Mid.WebApi.Data;
using AbySalto.Mid.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AbySalto.Mid.WebApi.Services
{
    public class BasketService
    {
        private readonly ApplicationDbContext _context;

        public BasketService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddProductAsync(int userId, int productId, int quantity)
        {
            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.UserId == userId && b.ProductId == productId);
            if (basketItem != null)
            {
                basketItem.Quantity += quantity;
            }
            else
            {
                basketItem = new BasketItem { UserId = userId, ProductId = productId, Quantity = quantity };
                _context.BasketItems.Add(basketItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task RemoveProductAsync(int userId, int productId)
        {
            var basketItem = await _context.BasketItems.FirstOrDefaultAsync(b => b.UserId == userId && b.ProductId == productId);
            if (basketItem != null)
            {
                _context.BasketItems.Remove(basketItem);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<ProductWithQuantity>> GetProductsFromBasketAsync(int userId)
        {
            // Fetch BasketItems for the user
            var basketItems = await _context.BasketItems
                .Where(bi => bi.UserId == userId)
                .ToListAsync();

            // Group by ProductId and sum quantities
            var groupedItems = basketItems
                .GroupBy(bi => bi.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    TotalQuantity = g.Sum(bi => bi.Quantity)
                })
                .ToList();

            // Fetch the products for these product IDs
            var productIds = groupedItems.Select(g => g.ProductId).ToList();

            ProductService productService = new ProductService(new MemoryCache(new MemoryCacheOptions()), new HttpClient());
            var productResponse = await productService.GetProductsAsync();
            var products = (productResponse.Products ?? new List<Product>())
                .Where(p => productIds.Contains(p.Id))
                .ToList();

            // Combine products with their counts
            var result = groupedItems
                .Join(products, g => g.ProductId, p => p.Id, (g, p) => new ProductWithQuantity
                {
                    Product = p,
                    Count = g.TotalQuantity
                })
                .ToList();

            return result;
        }

        public class ProductWithQuantity
        {
            public Product Product { get; set; }
            public int Count { get; set; }
        }
    }
}
