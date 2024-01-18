

using DTOs;
using Entidades;

namespace Logica.User
{
    public interface IUserLogic
    {
        UserItem Create(UserItemDto user);
        UserItem? GetForId(long id);
        UserItem? GetForUsername(string username);
        List<UserItem> GetList();
        void Delete(long id);
    }
}
