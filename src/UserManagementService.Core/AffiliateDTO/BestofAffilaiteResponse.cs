
namespace UserManagementService.Core.AffiliateDTO
{
    public class BestofAffilaiteResponse
    {
        public int RetailerId { get; set; }
        public string RetailerName { get; set; }
        public string BaseUrl { get; set; }
        public int DealId { get; set; }
        public decimal? CashbackFlat { get; set; }
        public decimal? CashbackPercentage { get; set; }
        public decimal? MaxCashbackFlat { get; set; }
        public decimal? MaxCashbackPercentage { get; set; }

        public string Title { get; set; }

    }
}