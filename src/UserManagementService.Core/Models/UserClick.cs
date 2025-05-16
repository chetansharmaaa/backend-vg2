using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class UserClick
{
    public int ClickId { get; set; }

    public int UserId { get; set; }

    public Guid LinkId { get; set; }

    public DateTime? ClickTime { get; set; }

    public virtual AffiliateLink Link { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
