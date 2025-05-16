using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagementService.Core.AffiliateDTO;
using UserManagementService.Core.RepositoryContract;
using UserManagementService.Core.ServiceContract;

namespace UserManagementService.app.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class affiliateController : ControllerBase
    {
        private readonly IAffiliateService _affiliateRepository;

        public affiliateController(IAffiliateService affiliateRepository)
        {
            _affiliateRepository = affiliateRepository;
        }

        [HttpGet]
        [Route("BestOfRetialers/{count}/{RetailerId}")]
        public async Task<IActionResult> GetBestOfRetailers([FromRoute] int count, [FromRoute]int RetailerId)
        {
            var response = await _affiliateRepository.BestofRetailerstop6(count,RetailerId);
            return Ok(response);
        }

        [HttpGet("Deals")]
        public async Task<IActionResult> GetFlashDeals()
        {
            var response = await _affiliateRepository.GetFlashDeals();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetLink/{dealId}/{userId}")]
        public async Task<IActionResult> GetAffiliateLink([FromRoute] int dealId,[FromRoute] int userId)
        {
            var link = await _affiliateRepository.GenerateAffiliateLink(dealId, userId);
            return Ok(link.RedirectUrl);
        }

        [HttpPost]
        [Route("tracking/Purchase")]
        public    IActionResult RecordPurchase([FromBody] PurchaseRequestDTO purchaseRequestDTO)
        {
             _affiliateRepository.TrackPurchase(purchaseRequestDTO.UserId, purchaseRequestDTO.LinkId, purchaseRequestDTO.Amount);
            return Ok("Updated DB");
        }

        [HttpPatch]
        [Route("AddDeals")]
        public async Task<IActionResult> AddDeals(DealRequestDTO dealRequestDTO) 
        {

            var response = await _affiliateRepository.addDeals(dealRequestDTO);
            return Ok(response);
                
              }

        [HttpPut]
        [Route("UpdateDeal/{dealId}")]
        public async Task<IActionResult> updateDeals(int dealId , DealRequestDTO dealRequestDTO)
        {
            var response = await _affiliateRepository.updateDeals(dealId, dealRequestDTO);
            return Ok(response);
        }
        [HttpDelete]
        [Route("DeleteDeal/{dealId}")]
        public async Task<IActionResult> deleteDeal(int dealId)
        {
            var response = await _affiliateRepository.deleteDeals(dealId);
            return Ok(response);
        }

        [HttpGet]
        [Route("TopDeals")]
        public IActionResult GetTopDeals()
        {
            var response = _affiliateRepository.topDeals();
            return Ok(response);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _affiliateRepository.GetAllCategory();
            return Ok(response);
        }

        [HttpPost]
        [Route("AddCategory")]
        public async  Task<IActionResult> AddCategory(CategoryRequestDTO categoryRequestDTO)
        {
            var response = await _affiliateRepository.addCategory(categoryRequestDTO);
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateCategory/{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId , CategoryRequestDTO categoryRequestDTO)
        {
            var response = await _affiliateRepository.updateCategory(categoryId, categoryRequestDTO);
            return Ok(response);
        }

        [HttpPost]
        [Route("AddRetailer/{categoryId}")]
        public async Task<IActionResult> AddRetailer(int categoryId, RetailerRequestDTO retailerRequestDTO)
        {
            var response = await _affiliateRepository.addRetailer(categoryId, retailerRequestDTO);
            return Ok(response);
        }

        [HttpPut]
        [Route("UpdateRetailer/{retailerId}")]
        public async Task<IActionResult> UpdateRetailer(int retailerId, RetailerRequestDTO retailerRequestDTO)
        {
            var response = await _affiliateRepository.updateRetailer(retailerId, retailerRequestDTO);
            return Ok(response);
        }

        [HttpDelete]
        [Route("DeleteRetailer/{retailerId}")]  
        public async Task<IActionResult> DeleteRetailer(int retailerId)
        {
            var response = await _affiliateRepository.deleteRetailer(retailerId);
            return Ok(response);
        }


    }
}
