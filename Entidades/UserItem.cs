using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class UserItem
    {
        public long Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public DateTime? ExpiredAt { get; set; }
        public string DisplayName { get; set; } = string.Empty;
        public string? Role { get; set; }
    }
}
