using Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.Models;
using UserManagementService.Core.UserDTO;

namespace UserManagementService.Core.RepositoryContract
{
    public interface IAffiliateRepository
    {
       Task<AffiliateLink> CreateLink(int dealId, int userId);

        void TrackClick(int userId, Guid linkId);

        void TrackPurchase(int userId, Guid linkId, decimal purchaseAmount);

        Task<List<Deal>> GetFlashDeals();
        Task<response> AddDeals(DealRequestDTO dealRequestDTO);

        Task<response> UpdateDeals(int dealId, DealRequestDTO dealRequestDTO);

        Task<response> DeleteDeals(int dealId);

        List<Deal> topnthDeals();

        Task<response> AddCategory(CategoryRequestDTO categoryRequestDTO);

        Task<response> UpdateCategory(int categoryId, CategoryRequestDTO categoryRequestDTO);

        Task<response> AddRetailer(int categoryId , RetailerRequestDTO retailerRequestDTO);

        Task<response> UpdateRetailer(int retailerId, RetailerRequestDTO retailerRequestDTO);

        Task<response> DeleteRetailer(int retailerId);

        Task<List<CategoriesResponse>> GetCategories();

        Task<List<BestofRetailerGroup>> BestofRetailers(int topDealsCount ,int retailerId);
    }
}
