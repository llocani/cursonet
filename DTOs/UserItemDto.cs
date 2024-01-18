using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class UserItemDto
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? Role { get; set; }
        public string? Password { get; set; }

        //public UserItemDto(UserItemDto userItem)
        //{
        //    Id = userItem.Id;
        //    Username = userItem.Username;
        //    DisplayName = userItem.DisplayName;
        //    Role = userItem.Role;
        //}
    }
}
