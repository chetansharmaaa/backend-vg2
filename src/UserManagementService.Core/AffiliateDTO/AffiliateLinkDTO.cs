using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.AffiliateDTO
{
    public class AffiliateLinkDTO
    {
        
        public Guid LinkId { get; set; }

        public int DealId { get; set; }

        public string RedirectUrl { get; set; } = null!;

        public string? TrackingCode { get; set; }

        //public virtual Deal Deal { get; set; } = null!;

        //public virtual ICollection<UserClick> UserClicks { get; set; } = new List<UserClick>();

        //public virtual ICollection<UserPurchase> UserPurchases { get; set; } = new List<UserPurchase>();
  

        

    }
    public static class AffiliateLinkDTOExtension
    {
        public static AffiliateLinkDTO ToAffiliateLinkDTO(this AffiliateLink affiliateLink)
        {
            return new AffiliateLinkDTO
            {
                LinkId = affiliateLink.LinkId,
                DealId = affiliateLink.DealId,
                RedirectUrl = affiliateLink.RedirectUrl,
                TrackingCode = affiliateLink.TrackingCode
            };
        }
    }
}
