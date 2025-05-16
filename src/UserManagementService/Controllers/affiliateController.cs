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

        [HttpGet("Deals")]
        public IActionResult GetFlashDeals()
        {
            return Ok(_affiliateRepository.FlashDeals());
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
        public  IActionResult RecordPurchase([FromBody] PurchaseRequestDTO purchaseRequestDTO)
        {
            _affiliateRepository.TrackPurchase(purchaseRequestDTO.UserId, purchaseRequestDTO.LinkId, purchaseRequestDTO.Amount);
            return Ok("Updated DB");
        }

    }
}
