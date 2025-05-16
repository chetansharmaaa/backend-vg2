using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.Models;
using UserManagementService.Core.UserDTO;

namespace UserManagementService.Core.Extensionclass
{
    public static class AffiliateLinkExtensions
    {
        public static AffialateLinkResponse ToAffiliateLinkResponse(this AffiliateLink affiliateLink)
        {
            return new AffialateLinkResponse
            {
                LinkId = affiliateLink.LinkId,
                DealId = affiliateLink.DealId,
                RedirectUrl = affiliateLink.RedirectUrl,
                TrackingCode = affiliateLink.TrackingCode
            };
        }
    }
}
