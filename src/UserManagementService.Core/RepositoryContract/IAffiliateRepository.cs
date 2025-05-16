using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.RepositoryContract
{
    public interface IAffiliateRepository
    {
       Task<AffiliateLink> CreateLink(int dealId, int userId);

        void TrackClick(int userId, Guid linkId);

        void TrackPurchase(int userId, Guid linkId, decimal purchaseAmount);

        Task<List<Deal>> GetFlashDeals();

    }
}
