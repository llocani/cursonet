using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Stores;
using DTOs;
using Mapeos;

namespace cursonetcore.Controllers
{
    // Clase 9: La decoración [Authorize] indica que para los endpoints definidos en este controlador hay que estar autentificado
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUserStore _userStore;
        private readonly IMapeosUser _mapeosUser;

        public UserController(
            ILogger<UserController> logger,
            IUserStore userStore, IMapeosUser mapeosUser)
        {
            _logger = logger;
            _userStore = userStore;
            _mapeosUser = mapeosUser;
        }

        [HttpPost]
        public UserItemDto Create(UserItemDto userDto)
        {
            var user = _mapeosUser.UserItemaDtoAUserItem(userDto);
            _userStore.InsertUser(user);
            userDto = _mapeosUser.UserItemaAUserItemDto(user);

            return userDto;
        }

        [HttpGet]
        public IEnumerable<UserItemDto> Get()
        {
            return _userStore.GetAllUser().Select(user => _mapeosUser.UserItemaAUserItemDto(user));
        }

    }
}
