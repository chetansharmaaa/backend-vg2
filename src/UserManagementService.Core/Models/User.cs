using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public decimal? TotalCashback { get; set; }

    public virtual ICollection<CashbackTransaction> CashbackTransactions { get; set; } = new List<CashbackTransaction>();

    public virtual ICollection<MissingCashbackReport> MissingCashbackReports { get; set; } = new List<MissingCashbackReport>();

    public virtual ICollection<OrderCashback> OrderCashbacks { get; set; } = new List<OrderCashback>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Referral> ReferralReferredUsers { get; set; } = new List<Referral>();

    public virtual ICollection<Referral> ReferralReferrerUsers { get; set; } = new List<Referral>();

    public virtual ICollection<UserClick> UserClicks { get; set; } = new List<UserClick>();

    public virtual ICollection<UserPurchase> UserPurchases { get; set; } = new List<UserPurchase>();

    public virtual ICollection<UserRating> UserRatings { get; set; } = new List<UserRating>();
}
