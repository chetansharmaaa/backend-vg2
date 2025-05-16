using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int UserId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public string? PaymentMethod { get; set; }

    public string? TransactionId { get; set; }

    public virtual ICollection<OrderCashback> OrderCashbacks { get; set; } = new List<OrderCashback>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual User User { get; set; } = null!;
}
