using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class UserPurchase
{
    public int PurchaseId { get; set; }

    public int UserId { get; set; }

    public Guid LinkId { get; set; }

    public decimal PurchaseAmount { get; set; }

    public decimal CashbackEarned { get; set; }

    public DateTime? PurchaseTime { get; set; }

    public string? Status { get; set; }

    public virtual AffiliateLink Link { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
