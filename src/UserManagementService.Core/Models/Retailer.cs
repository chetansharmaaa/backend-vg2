using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class Retailer
{
    public int RetailerId { get; set; }

    public string Name { get; set; } = null!;

    public string? AffiliateNetwork { get; set; }

    public string? BaseUrl { get; set; }

    public virtual ICollection<Deal> Deals { get; set; } = new List<Deal>();
}
