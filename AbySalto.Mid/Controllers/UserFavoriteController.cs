using AbySalto.Mid.WebApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AbySalto.Mid.WebApi.Controllers
{
    public class UserFavoriteController : ControllerBase
    {
        private readonly UserFavoriteService _favoriteService;

        public UserFavoriteController(UserFavoriteService favoriteService)
        {
            _favoriteService = favoriteService;
        }

    }
}
