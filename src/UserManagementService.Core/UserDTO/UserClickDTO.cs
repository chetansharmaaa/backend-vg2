using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.UserDTO
{
    public class UserClickDTO
    {
        public int ClickId { get; set; }

        public int UserId { get; set; }

        public Guid LinkId { get; set; }

        public DateTime? ClickTime { get; set; }

        public virtual AffialateLinkResponse Link { get; set; } 

     
    }
}
