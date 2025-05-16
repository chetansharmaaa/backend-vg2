using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.UserDTO
{
    public class updateUserDTO
    {
         
        public string FullName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string PasswordHash { get; set; } = null!;

        public string Role { get; set; } = null!;

        public User ToModel()
        {
            return new User
            {
                FullName = FullName,
                Email = Email,
                PasswordHash = PasswordHash,
                Role = Role,
                CreatedAt = DateTime.Now
            };

        }

    }
}
