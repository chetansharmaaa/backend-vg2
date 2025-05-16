using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class RetailerCategory
{
    public int RetailerId { get; set; }

    public int CategoryId { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual Retailer Retailer { get; set; } = null!;
}
