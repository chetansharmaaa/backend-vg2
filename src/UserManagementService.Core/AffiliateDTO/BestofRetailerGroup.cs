using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.Core.AffiliateDTO
{
    public  class BestofRetailerGroup
    {
        public int RetailerId { get; set; }
        public string RetailerName { get; set; }
        public string BaseUrl { get; set; }
        public string Title { get; set; }

        public List<DealResponse> Deals { get; set; }
        public decimal? TotalCashback { get; internal set; }
    }
}
