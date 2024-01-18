using DTOs;
using Entidades;
using Mapeos;
using Stores;

namespace Logica.User
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserStore _userStore;
        private readonly IMapeosUser _mapeosUser;
        
        public UserLogic(IUserStore userStore, IMapeosUser mapeosUser)
        {
            _mapeosUser = mapeosUser;
            _userStore = userStore;
        }

        UserItem IUserLogic.Create(UserItemDto userDto)
        {
            if (String.IsNullOrWhiteSpace(userDto.Username))
            {
                throw new ArgumentNullException(nameof(userDto.Username));
            }

            if (String.IsNullOrWhiteSpace(userDto.Password))
            {
                throw new ArgumentNullException(nameof(userDto.Password));
            }

            if (String.IsNullOrWhiteSpace(userDto.DisplayName))
            {
                throw new ArgumentNullException(nameof(userDto.DisplayName));
            }

            var users = _userStore.GetAllUser().Where(user => userDto.Username == user.Username);
            if (users.Count() > 0) {
                throw new ArgumentException("A user already with that username already exists.");
            }

            UserItem userItem = _mapeosUser.UserItemaDtoAUserItem(userDto);
            userItem.PasswordHash = PasswordHash.Hash(userDto.Password);
            _userStore.InsertUser(userItem);

            return userItem;
        }

        List<UserItem> IUserLogic.GetList()
        {
            return (from u in _userStore.GetAllUser()
                   where u.ExpiredAt == null || u.ExpiredAt > DateTime.Now
                   select u).ToList();
        }

        UserItem? IUserLogic.GetForId(long id)
        {
            return (from u in _userStore.GetAllUser()
                    where (u.ExpiredAt == null || u.ExpiredAt > DateTime.Now)
                        && u.Id == id
                    select u).FirstOrDefault();
        }

        UserItem? IUserLogic.GetForUsername(string username)
        {
            
            var todosLosUsuarios = _userStore.GetAllUser();
            var usuarios = from u in todosLosUsuarios
                           where (u.ExpiredAt == null || u.ExpiredAt > DateTime.Now)
                               && u.Username == username
                           select u;
            var usuario_elegido= usuarios.FirstOrDefault();
            return usuario_elegido;
        }

        void IUserLogic.Delete(long id)
        {
            UserItem? user = ((IUserLogic)this).GetForId(id);
            if (user == null)
            {
                throw new ArgumentException("The user with that ID does not exist.");
            }

            user.ExpiredAt = DateTime.Now;

            _userStore.UpdateUser(user);
        }
    }
}
