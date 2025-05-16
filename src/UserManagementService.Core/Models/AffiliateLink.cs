using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class AffiliateLink
{
    public Guid LinkId { get; set; }

    public int DealId { get; set; }

    public string RedirectUrl { get; set; } = null!;

    public string? TrackingCode { get; set; }

    public virtual Deal Deal { get; set; } = null!;

    public virtual ICollection<UserClick> UserClicks { get; set; } = new List<UserClick>();

    public virtual ICollection<UserPurchase> UserPurchases { get; set; } = new List<UserPurchase>();
}
