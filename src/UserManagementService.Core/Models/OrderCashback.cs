using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class OrderCashback
{
    public int CashbackId { get; set; }

    public int OrderId { get; set; }

    public int UserId { get; set; }

    public decimal CashbackAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? ApprovedDate { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
