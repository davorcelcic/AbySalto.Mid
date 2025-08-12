using AbySalto.Mid.WebApi.Data;
using AbySalto.Mid.WebApi.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}
