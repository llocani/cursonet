using cursonetcore.IServicios;
using Entidades;
using Stores;

namespace cursonetcore.Servicios
{
    public class UserServicio : IUserServicio
    {
        private readonly UserStore _userStore;
        public UserServicio(UserStore userStore)
        {
            _userStore = userStore;
        }
    }
}
