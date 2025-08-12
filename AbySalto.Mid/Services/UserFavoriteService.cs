using AbySalto.Mid.WebApi.Data;
using AbySalto.Mid.WebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;

namespace AbySalto.Mid.WebApi.Services
{
    public class UserFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public UserFavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddFavoriteAsync(int userId, int productId)
        {
            var exists = await _context.UserFavorites.AnyAsync(uf => uf.UserId == userId && uf.ProductId == productId);
            if (exists)
                return false;

            _context.UserFavorites.Add(new UserFavorite { UserId = userId, ProductId = productId });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> RemoveFavoriteAsync(int userId, int productId)
        {
            var fav = await _context.UserFavorites.FirstOrDefaultAsync(uf => uf.UserId == userId && uf.ProductId == productId);
            if (fav == null)
                return false;

            _context.UserFavorites.Remove(fav);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Product>> GetFavoriteProductsAsync(int userId)
        {
            // Fetch the FavoriteItems for the user
            var favoriteProductIds = await _context.UserFavorites
                .Where(f => f.UserId == userId)
                .Select(f => f.ProductId)
                .ToListAsync();

            // Fetch the products with those IDs 
            ProductService productService = new ProductService(new MemoryCache(new MemoryCacheOptions()), new HttpClient());
            var productResponse = await productService.GetProductsAsync();
            var products = (productResponse.Products ?? new List<Product>())
                .Where(p => favoriteProductIds.Contains(p.Id))
                .ToList();

            return products;
        }
    }
}
