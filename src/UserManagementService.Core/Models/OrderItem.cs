using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class OrderItem
{
    public int OrderItemId { get; set; }

    public int OrderId { get; set; }

    public string ProductName { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public bool? CashbackEligible { get; set; }

    public decimal? CashbackAmount { get; set; }

    public virtual Order Order { get; set; } = null!;
}
