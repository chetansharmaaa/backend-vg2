using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.Data;
using UserManagementService.Core.Models;
using UserManagementService.Core.RepositoryContract;

namespace UserManagementService.Infra
{
    public class AffiliateRepository : IAffiliateRepository
    {
        private readonly CoolpalzContext _dbContext;

        public AffiliateRepository(CoolpalzContext dbContext)
        {
            _dbContext = dbContext;
        }
        /// <summary>
        /// Code to Create Affiliate Link
        /// </summary>
        /// <param name="dealId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<AffiliateLink> CreateLink(int dealId, int userId)
        {
            //var deal =await _dbContext.Deals.FindAsync(dealId);
            var deal = await _dbContext.Deals.Include(d => d.Retailer).FirstOrDefaultAsync(d => d.DealId == dealId);

            var trackingCode = $"{userId}-{Guid.NewGuid()}"; //Unique Code

            var affiliateLink = new AffiliateLink
            {
                Deal = deal,
                TrackingCode = trackingCode,
                RedirectUrl = $"{deal.Retailer.BaseUrl}?affiliate={trackingCode}"
            };

            _dbContext.AffiliateLinks.Add(affiliateLink);
           await _dbContext.SaveChangesAsync();
            return affiliateLink;
        }

        public async Task<List<Deal>> GetFlashDeals()
        {
            return _dbContext.Deals.Where(d => d.IsFlashDeal == true && d.EndDate >DateTime.UtcNow).ToList();
        }


        /// <summary>
        /// Code to Track User CLick For
        /// a Particular Link
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="linkId"></param>
        public async void TrackClick(int userId, Guid linkId)
        {
          await  _dbContext.UserClicks.AddAsync(new UserClick
            {
                UserId = userId,
                LinkId = linkId,
                ClickTime = DateTime.Now
            });
            await _dbContext.SaveChangesAsync();
        }


        /// <summary>
        /// Code To TRack Purchase Amount
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="linkId"></param>
        /// <param name="purchaseAmount"></param>
        public void TrackPurchase(int userId, Guid linkId, decimal purchaseAmount)
        {
            var link = _dbContext.AffiliateLinks.Where(x => x.LinkId == linkId).Include(l => l.Deal).FirstOrDefault(l => l.LinkId == linkId);
            decimal cashback = CalculateCashback(link.Deal, purchaseAmount);

            _dbContext.UserPurchases.Add(
                new UserPurchase
                {
                    UserId = userId,
                    LinkId = linkId,
                    PurchaseAmount = purchaseAmount,
                    CashbackEarned = cashback,
                    Status = "Completed"
                }
                );
            User user = _dbContext.Users.Find(userId);
            user.TotalCashback += cashback;
            _dbContext.SaveChanges();

        }
        private decimal CalculateCashback(Deal deal, decimal purchaseAmount)
        {
            if (deal.CashbackPercentage.HasValue) { 
            return purchaseAmount*deal.CashbackPercentage.Value/100;
            
            }
            else
            {
                return deal.CashbackFlat ?? 0;
            }
        }
    }
}
