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
    public static class ToUserPurchaseExtension
    {
        public static UserPurchaseDTO ToUserPurchaseDTO(this UserPurchase userPurchase)
        {
            return new UserPurchaseDTO
            {
                PurchaseId = userPurchase.PurchaseId,
                UserId = userPurchase.UserId,
                LinkId = userPurchase.LinkId,
                PurchaseAmount = userPurchase.PurchaseAmount,
                PurchaseTime = userPurchase.PurchaseTime,
                CashbackEarned = userPurchase.CashbackEarned
                //Link = userPurchase.Link.ToAffiliateLinkDTO(),
            };
        }
    }
}
