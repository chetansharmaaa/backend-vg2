using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class MissingCashbackReport
{
    public int ReportId { get; set; }

    public int UserId { get; set; }

    public decimal Amount { get; set; }

    public string? Details { get; set; }

    public string? Status { get; set; }

    public DateTime? ReportDate { get; set; }

    public virtual User User { get; set; } = null!;
}
