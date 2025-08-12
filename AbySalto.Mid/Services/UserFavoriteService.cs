using AbySalto.Mid.WebApi.Data;
using AbySalto.Mid.WebApi.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
