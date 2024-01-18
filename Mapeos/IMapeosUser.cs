using DTOs;
using Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapeos
{
    public interface IMapeosUser
    {
        public UserItemDto UserItemaAUserItemDto(UserItem UserItem);
        public UserItem UserItemaDtoAUserItem(UserItemDto UserItem);
    }
}
