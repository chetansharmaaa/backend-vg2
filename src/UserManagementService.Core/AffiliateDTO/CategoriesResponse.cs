namespace UserManagementService.Core.AffiliateDTO
{
    public class CategoriesResponse
    {
        public int CategoryId { get; set; }

        public string Name { get; set; } = null!;

        public string? ImageUrl { get; set; }
    }
}