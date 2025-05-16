using Azure;
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
using UserManagementService.Core.UserDTO;

namespace UserManagementService.Infra
{
    public class AffiliateRepository : IAffiliateRepository
    {
        private readonly CoolpalzContext _dbContext;

        public AffiliateRepository(CoolpalzContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<response> AddCategory(CategoryRequestDTO categoryRequestDTO)
        {
            var category = new Category
            {
                // Map properties from categoryRequestDTO to Category
                Name = categoryRequestDTO.Name,
                ImageUrl = categoryRequestDTO.ImageUrl,
            };

            _dbContext.Categories.Add(category);
            await _dbContext.SaveChangesAsync();

            return new response
            {
                Success = true,
                Message = "Category added successfully."
            };
        }

        public async Task<response> AddDeals(DealRequestDTO dealRequestDTO)
        {
            var deal = new Deal
            {
                RetailerId = dealRequestDTO.RetailerId,
                IsFlashDeal = dealRequestDTO.IsFlashDeal,
                CashbackPercentage = dealRequestDTO.CashbackPercentage,
                CashbackFlat = dealRequestDTO.CashbackFlat,
                StartDate = dealRequestDTO.StartDate,
                EndDate = dealRequestDTO.EndDate,
                Title = dealRequestDTO.Title

            };

            _dbContext.Deals.Add(deal);
            await _dbContext.SaveChangesAsync();

            return new response
            {
                Success = true,
                Message = "Deal added successfully."
            };
        }

        public async Task<response> AddRetailer(int categoryId, RetailerRequestDTO retailerRequestDTO)
        {
            var retailer = new Retailer
            {
                Name = retailerRequestDTO.Name,
                BaseUrl = retailerRequestDTO.BaseUrl,
               
            };
             

            _dbContext.Retailers.Add(retailer);
            await _dbContext.SaveChangesAsync();
            var lastRetailer = await _dbContext.Retailers.OrderByDescending(r => r.RetailerId).FirstOrDefaultAsync();
            RetailerCategory retailerCategory = new RetailerCategory
            {
                RetailerId = lastRetailer.RetailerId,
                CategoryId = categoryId
            };
            _dbContext.RetailerCategories.Add(retailerCategory);
            await _dbContext.SaveChangesAsync();

            return new response
            {
                Success = true,
                Message = "Retailer added successfully."
            };
        }

        public async Task<List<BestofRetailerGroup>> BestofRetailers(int count ,int retailerId)
        {
            try
            {
                var deals = await _dbContext.BestRetailerDeals
                    .FromSqlInterpolated($"EXEC [dbo].[sp_GetBestRetailerDeals] @TopDealsCount = {count}")
                    .AsNoTracking() // Optional: improve performance for read-only queries
                    .ToListAsync();
                var retailerGroups = deals.Where(i => i.RetailerId == retailerId)
        .GroupBy(r => r.RetailerId)
        .Select(g => new BestofRetailerGroup
        {
            RetailerId = g.Key,
            RetailerName = g.First().RetailerName,
            BaseUrl = g.First().BaseUrl,
            Title = g.First().Title,
            Deals = g.Select(d => new DealResponse
            {
                DealId = d.DealId,
                RetailerId = d.RetailerId,
                CashbackPercentage = d.CashbackPercentage,
                CashbackFlat = d.CashbackFlat,
                Title = d.Title
             
            }).ToList(),
         })
         .ToList();
                return retailerGroups;

            }
            catch (Exception ex)
            {
                 throw new Exception("Failed to retrieve best retailer deals.", ex);
            }
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

        public async Task<response> DeleteDeals(int dealId)
        {
            var deal = await _dbContext.Deals.FindAsync(dealId);
            if (deal == null)
            {
                return new response
                {
                    Success = false,
                    Message = "Deal not found."
                };
            }

            _dbContext.Deals.Remove(deal);
            await _dbContext.SaveChangesAsync();

            return new response
            {
                Success = true,
                Message = "Deal deleted successfully."
            };
        }

        public async Task<response> DeleteRetailer(int retailerId)
        {
            var retailer = await _dbContext.Retailers.FindAsync(retailerId);
            if (retailer == null)
            {
                return new response
                {
                    Success = false,
                    Message = "Retailer not found."
                };
            }

            _dbContext.Retailers.Remove(retailer);
            await _dbContext.SaveChangesAsync();

            return new response
            {
                Success = true,
                Message = "Retailer deleted successfully."
            };
        }

        public async Task<List<CategoriesResponse>> GetCategories()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            return categories.Select(c => new CategoriesResponse
            {
                CategoryId = c.CategoryId,
                Name = c.Name,
                ImageUrl = c.ImageUrl
            }).ToList();
        }

        public async Task<List<Deal>> GetFlashDeals()
        {
            try
            {
                List<Deal> response = await _dbContext.Deals.Where(d => d.IsFlashDeal == true ).ToListAsync();
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while retrieving flash deals.", ex);
            }
        }

        public     List<Deal> topnthDeals()
        {

            try
            {
                List<Deal> response = (from deal in _dbContext.Deals
                                       where deal.CashbackFlat != null && deal.IsFlashDeal != null
                                       orderby deal.CashbackFlat descending
                                       select new Deal
                                       {
                                           DealId = deal.DealId,
                                           RetailerId = deal.RetailerId,
                                           Title = deal.Title,
                                           CashbackPercentage = deal.CashbackPercentage,
                                           CashbackFlat = deal.CashbackFlat,
                                           IsFlashDeal = deal.IsFlashDeal
                                       }).ToList();

                return response;
            }
            catch (Exception ex)
            {

                throw new Exception("An error occurred while retrieving flash deals.", ex);
            }
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

        public async Task<response> UpdateCategory(int categoryId, CategoryRequestDTO categoryRequestDTO)
        {
            var category = await _dbContext.Categories.FindAsync(categoryId);
            if (category == null)
            {
                return new response
                {
                    Success = false,
                    Message = "Category not found."
                };
            }

            category.Name = categoryRequestDTO.Name;
            category.ImageUrl = categoryRequestDTO.ImageUrl;

            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();

            return new response
            {
                Success = true,
                Message = "Category updated successfully."
            };
        }

        public async  Task<response> UpdateDeals(int dealId, DealRequestDTO dealRequestDTO)
        {
            var deal = await _dbContext.Deals.FindAsync(dealId);
            if (deal == null)
            {
                return new response
                {
                    Success = false,
                    Message = "Deal not found."
                };
            }

            deal.RetailerId = dealRequestDTO.RetailerId;
            deal.IsFlashDeal = dealRequestDTO.IsFlashDeal;
            deal.CashbackPercentage = dealRequestDTO.CashbackPercentage;
            deal.CashbackFlat = dealRequestDTO.CashbackFlat;
            deal.StartDate = dealRequestDTO.StartDate;
            deal.EndDate = dealRequestDTO.EndDate;

            _dbContext.Deals.Update(deal);
            await _dbContext.SaveChangesAsync();

            return new response
            {
                Success = true,
                Message = "Deal updated successfully."
            };
        }

        public async Task<response> UpdateRetailer(int retailerId, RetailerRequestDTO retailerRequestDTO)
        {
            var retailer = await _dbContext.Retailers.FindAsync(retailerId);
            if (retailer == null)
            {
                return new response
                {
                    Success = false,
                    Message = "Retailer not found."
                };
            }

            retailer.Name = retailerRequestDTO.Name;
            retailer.BaseUrl = retailerRequestDTO.BaseUrl;
            retailer.AffiliateNetwork = retailerRequestDTO.AffiliateNetwork;

            _dbContext.Retailers.Update(retailer);
            await _dbContext.SaveChangesAsync();

            return new response
            {
                Success = true,
                Message = "Retailer updated successfully."
            };
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
