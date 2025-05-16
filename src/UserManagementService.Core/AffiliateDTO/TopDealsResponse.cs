namespace UserManagementService.Core.AffiliateDTO
{
    public class TopDealsResponse
    {
        public int RetailerId { get; set; }

        public string Title { get; set; } = null!;

        public decimal? CashbackPercentage { get; set; }

        public decimal? CashbackFlat { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool? IsFlashDeal { get; set; }
    }
}