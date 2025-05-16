using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.AffiliateDTO
{
    public class DealResponseDTO
    {

        public int RetailerId { get; set; }

        public string Title { get; set; } = null!;

        public decimal? CashbackPercentage { get; set; }

        public decimal? CashbackFlat { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsFlashDeal { get; set; }

       // public virtual ICollection<AffiliateLink> AffiliateLinks { get; set; } = new List<AffiliateLink>();

        public virtual Retailer Retailer { get; set; } = null!;
    }
}
