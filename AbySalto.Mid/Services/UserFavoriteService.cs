using AbySalto.Mid.WebApi.Data;

namespace AbySalto.Mid.WebApi.Services
{
    public class UserFavoriteService
    {
        private readonly ApplicationDbContext _context;

        public UserFavoriteService(ApplicationDbContext context)
        {
            _context = context;
        }

    }
}
