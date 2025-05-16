using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.Extensionclass
{
    public static class dealExtensioncs
    {
        public static DealResponseDTO ToDealResponseDTO(this Deal deal)
        {
            return new DealResponseDTO
            {
                RetailerId = deal.RetailerId,
                Title = deal.Title,
                CashbackPercentage = deal.CashbackPercentage,
                CashbackFlat = deal.CashbackFlat,
                StartDate = deal.StartDate,
                EndDate = deal.EndDate,
                IsFlashDeal = deal.IsFlashDeal,
                //Retailer = deal.Retailer
            };
        }
    }
}
