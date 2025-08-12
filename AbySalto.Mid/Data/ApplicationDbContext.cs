using AbySalto.Mid.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.WebApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users => Set<User>();
        public DbSet<UserFavorite> UserFavorites => Set<UserFavorite>();
        public DbSet<BasketItem> BasketItems => Set<BasketItem>();
    }
}
