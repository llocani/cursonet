using Entidades;

namespace Stores
{
    public interface IUserStore
    {
        public List<UserItem> GetAllUser();
        public UserItem InsertUser(UserItem userItem);
        public UserItem UpdateUser(UserItem userItem);
    }
}
