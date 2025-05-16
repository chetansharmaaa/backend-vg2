using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.Extensions
{
    public static class DealExtensions
    {
        public static DealResponse ToDealResponse(this Deal deal)
        {
            return new DealResponse
            {
                RetailerId = deal.RetailerId,
                Title = deal.Title,
                CashbackPercentage = deal.CashbackPercentage,
                CashbackFlat = deal.CashbackFlat,
                StartDate = deal.StartDate,
                EndDate = deal.EndDate,
                IsFlashDeal = deal.IsFlashDeal
            };
        }
    }
}



