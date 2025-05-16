using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;
using UserManagementService.Core.UserDTO;

namespace UserManagementService.Core.Extensionclass
{
    public static class UserCLickExtension
    {
        public static UserClickDTO ToUserClickDTO(this UserClick userClick)
        {
            return new UserClickDTO
            {
                ClickId = userClick.ClickId,
                UserId = userClick.UserId,
                LinkId = userClick.LinkId,
                ClickTime = userClick.ClickTime,
                Link = userClick.Link.ToAffiliateLinkResponse(),
                //Link = userClick.Link.ToAffiliateLinkDTO(),
            };
        }
    }
}
