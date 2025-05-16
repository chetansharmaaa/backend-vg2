using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.Extensionclass;
using UserManagementService.Core.Models;
using UserManagementService.Core.RepositoryContract;
using UserManagementService.Core.ServiceContract;

namespace UserManagementService.Core.Service
{
    public class AffiliateService : IAffiliateService
    {
        private readonly IAffiliateRepository _affiliateRepository;

        public AffiliateService(IAffiliateRepository affiliateRepository)
        {
            _affiliateRepository = affiliateRepository;
        }
        public async Task<AffiliateLinkDTO> GenerateAffiliateLink(int dealId, int userId)
        {
            AffiliateLink affiliateLink = await _affiliateRepository.CreateLink(dealId, userId);

            return affiliateLink.ToAffiliateLinkDTO();
        }

        public async Task<List<DealResponseDTO>> FlashDeals()
        {
            var response = await _affiliateRepository.GetFlashDeals();
            return response.Select(x => x.ToDealResponseDTO()).ToList();
        }

        public void TrackClick(int userId, Guid linkId)
        {
            _affiliateRepository.TrackClick(userId, linkId);
        }

        public void TrackPurchase(int userId, Guid linkId, decimal purchaseAmount)
        {
            _affiliateRepository.TrackPurchase(userId, linkId, purchaseAmount);
        }
    }
}
