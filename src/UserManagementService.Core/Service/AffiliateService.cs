using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.Extensionclass;
using UserManagementService.Core.Models;
using UserManagementService.Core.RepositoryContract;
using UserManagementService.Core.ServiceContract;
using UserManagementService.Core.UserDTO;

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

        

        public void TrackClick(int userId, Guid linkId)
        {
            _affiliateRepository.TrackClick(userId, linkId);
        }

        public void TrackPurchase(int userId, Guid linkId, decimal purchaseAmount)
        {
            _affiliateRepository.TrackPurchase(userId, linkId, purchaseAmount);
        }

        public async Task<List<DealResponse>> GetFlashDeals()
        {
            var deals = await _affiliateRepository.GetFlashDeals();
            return deals.Select(d => new DealResponse
            {
                RetailerId = d.RetailerId,
                Title = d.Title,
                CashbackPercentage = d.CashbackPercentage,
                CashbackFlat = d.CashbackFlat,
                IsFlashDeal = d.IsFlashDeal,
                StartDate = d.StartDate,
                EndDate = d.EndDate,
            }).ToList();
        }

        public async Task<response> addDeals(DealRequestDTO dealRequestDTO)
        {
            return await _affiliateRepository.AddDeals(dealRequestDTO);
        }

        public async Task<response> updateDeals(int dealId, DealRequestDTO dealRequestDTO)
        {
            return await _affiliateRepository.UpdateDeals(dealId, dealRequestDTO);
        }

        public async Task<response> deleteDeals(int dealId)
        {
            return await _affiliateRepository.DeleteDeals(dealId);
        }

        public async Task<List<DealResponse>> topDeals()
        {
            var response =    _affiliateRepository.topnthDeals();
            List<DealResponse> deals = response.Select(d => new DealResponse
            {
                DealId = d.DealId,
                RetailerId = d.RetailerId,
                Title = d.Title,
                CashbackPercentage = d.CashbackPercentage,
                CashbackFlat = d.CashbackFlat,
                IsFlashDeal = d.IsFlashDeal
            }).ToList();
            return deals;
        }

        public async Task<response> addCategory(CategoryRequestDTO categoryRequestDTO)
        {
            return await _affiliateRepository.AddCategory(categoryRequestDTO);
        }

        public async Task<response> updateCategory(int categoryId, CategoryRequestDTO categoryRequestDTO)
        {
            return await _affiliateRepository.UpdateCategory(categoryId, categoryRequestDTO);
        }

        public async Task<response> addRetailer(int categoryId, RetailerRequestDTO retailerRequestDTO)
        {
            return await _affiliateRepository.AddRetailer(categoryId, retailerRequestDTO);
        }

        public async Task<response> updateRetailer(int retailerId, RetailerRequestDTO retailerRequestDTO)
        {
            return await _affiliateRepository.UpdateRetailer(retailerId, retailerRequestDTO);
        }

        public async Task<response> deleteRetailer(int retailerId)
        {
            return await _affiliateRepository.DeleteRetailer(retailerId);
        }

        public async Task<List<CategoriesResponse>> GetAllCategory()
        {
            return await _affiliateRepository.GetCategories();
        }

        public async Task<List<BestofRetailerGroup>> BestofRetailerstop6(int topDealsCount ,int retailerId)
        {
            var response = await _affiliateRepository.BestofRetailers(topDealsCount ,retailerId);

            return response;
        }
      
         
    }
}
