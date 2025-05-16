using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class CashbackTransaction
{
    public int TransactionId { get; set; }

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public string Source { get; set; } = null!;

    public DateTime? TransactionDate { get; set; }

    public virtual User User { get; set; } = null!;
}
