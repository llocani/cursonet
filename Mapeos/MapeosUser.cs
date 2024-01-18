using DTOs;
using Entidades;

namespace Mapeos
{
    public class MapeosUser : IMapeosUser
    {
        public UserItemDto UserItemaAUserItemDto(UserItem UserItem) => new UserItemDto()
        {
            DisplayName = UserItem.DisplayName,
            Id = UserItem.Id,
            Role = UserItem.Role,
            Username = UserItem.Username
        };
        public UserItem UserItemaDtoAUserItem(UserItemDto UserItem) => new UserItem()
        {
            DisplayName = UserItem.DisplayName,
            Id = UserItem.Id,
            Role = UserItem.Role,
            Username = UserItem.Username
        };

    }
}
