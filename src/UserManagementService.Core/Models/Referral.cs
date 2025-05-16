using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class Referral
{
    public int ReferralId { get; set; }

    public int ReferrerUserId { get; set; }

    public int ReferredUserId { get; set; }

    public decimal? RewardAmount { get; set; }

    public string? Status { get; set; }

    public DateTime? ReferralDate { get; set; }

    public virtual User ReferredUser { get; set; } = null!;

    public virtual User ReferrerUser { get; set; } = null!;
}
