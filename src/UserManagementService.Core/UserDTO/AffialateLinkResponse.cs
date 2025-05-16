using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.Core.UserDTO
{
    public class AffialateLinkResponse
    {
        public Guid LinkId { get; set; }

        public int DealId { get; set; }

        public string RedirectUrl { get; set; } = null!;

        public string? TrackingCode { get; set; }
    }
}
