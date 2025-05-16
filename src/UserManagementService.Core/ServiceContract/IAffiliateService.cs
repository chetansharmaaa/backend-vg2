using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.Models;
using UserManagementService.Core.UserDTO;

namespace UserManagementService.Core.ServiceContract
{
    public interface IAffiliateService
    {
        Task<AffiliateLinkDTO> GenerateAffiliateLink(int dealId, int userId);

        void TrackClick(int userId ,Guid linkId);   
        void TrackPurchase(int userId ,Guid linkId,decimal purchaseAmount);


        Task<List<DealResponse>> GetFlashDeals();

        Task<response> addDeals(DealRequestDTO dealRequestDTO);

        Task<response> updateDeals(int dealId, DealRequestDTO dealRequestDTO);

        Task<response> deleteDeals(int dealId);

        Task<List<DealResponse>> topDeals();

        Task<response> addCategory(CategoryRequestDTO categoryRequestDTO);

        Task<response> updateCategory(int categoryId, CategoryRequestDTO categoryRequestDTO);

        Task<response> addRetailer(int categoryId, RetailerRequestDTO retailerRequestDTO);

        Task<response> updateRetailer(int retailerId, RetailerRequestDTO retailerRequestDTO);

        Task<response> deleteRetailer(int retailerId);

        Task<List<CategoriesResponse>> GetAllCategory();

        Task<List<BestofRetailerGroup>> BestofRetailerstop6(int topDealsCount ,int retailerId);  
    }
}
