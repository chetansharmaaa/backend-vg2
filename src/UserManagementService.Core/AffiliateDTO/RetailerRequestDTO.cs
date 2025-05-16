using UserManagementService.Core.Models;

namespace UserManagementService.Core.AffiliateDTO
{
    public class RetailerRequestDTO
    {

        public string Name { get; set; } = null!;

        public string? AffiliateNetwork { get; set; }

        public string? BaseUrl { get; set; }

        //public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();
    }
}