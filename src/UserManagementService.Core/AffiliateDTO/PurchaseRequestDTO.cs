using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagementService.Core.AffiliateDTO
{
    public class PurchaseRequestDTO
    {
        public int UserId { get; set; }
        public Guid LinkId { get; set; } = Guid.NewGuid();

        public decimal Amount { get; set; }

    }
    
}
