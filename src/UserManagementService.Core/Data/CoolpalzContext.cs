using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using UserManagementService.Core.Models;

namespace UserManagementService.Core.Data;

public partial class CoolpalzContext : DbContext
{
    public CoolpalzContext()
    {
    }

    public CoolpalzContext(DbContextOptions<CoolpalzContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AffiliateLink> AffiliateLinks { get; set; }

    public virtual DbSet<CashbackTransaction> CashbackTransactions { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Deal> Deals { get; set; }

    public virtual DbSet<MissingCashbackReport> MissingCashbackReports { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderCashback> OrderCashbacks { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Referral> Referrals { get; set; }

    public virtual DbSet<Retailer> Retailers { get; set; }

    public virtual DbSet<RetailerCategory> RetailerCategories { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserClick> UserClicks { get; set; }

    public virtual DbSet<UserPurchase> UserPurchases { get; set; }

    public virtual DbSet<UserRating> UserRatings { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=CHETAN_SHARMA\\SQLEXPRESS;Initial Catalog=coolpalz;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AffiliateLink>(entity =>
        {
            entity.HasKey(e => e.LinkId).HasName("PK__Affiliat__2D122135648B5131");

            entity.Property(e => e.LinkId).HasDefaultValueSql("(newid())");
            entity.Property(e => e.TrackingCode).HasMaxLength(100);

            entity.HasOne(d => d.Deal).WithMany(p => p.AffiliateLinks)
                .HasForeignKey(d => d.DealId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Affiliate__DealI__2180FB33");
        });

        modelBuilder.Entity<CashbackTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Cashback__55433A6BA7B1CDE1");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Source).HasMaxLength(100);
            entity.Property(e => e.TransactionDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.CashbackTransactions)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CashbackT__UserI__5DCAEF64");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B5A111DDC");

            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Deal>(entity =>
        {
            entity.HasKey(e => e.DealId).HasName("PK__Deals__E5B2816670228843");

            entity.Property(e => e.CashbackFlat).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CashbackPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.IsFlashDeal).HasDefaultValue(false);
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Retailer).WithMany(p => p.Deals)
                .HasForeignKey(d => d.RetailerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Deals__RetailerI__1DB06A4F");
        });

        modelBuilder.Entity<MissingCashbackReport>(entity =>
        {
            entity.HasKey(e => e.ReportId).HasName("PK__MissingC__D5BD480574117CA4");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ReportDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.User).WithMany(p => p.MissingCashbackReports)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__MissingCa__UserI__6754599E");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF29788F46");

            entity.Property(e => e.OrderDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TransactionId).HasMaxLength(100);

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Orders__UserId__778AC167");
        });

        modelBuilder.Entity<OrderCashback>(entity =>
        {
            entity.HasKey(e => e.CashbackId).HasName("PK__OrderCas__FD6CE7666C063450");

            entity.ToTable("OrderCashback");

            entity.Property(e => e.ApprovedDate).HasColumnType("datetime");
            entity.Property(e => e.CashbackAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderCashbacks)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderCash__Order__0A9D95DB");

            entity.HasOne(d => d.User).WithMany(p => p.OrderCashbacks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderCash__UserI__0B91BA14");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("PK__OrderIte__57ED0681704F842B");

            entity.Property(e => e.CashbackAmount)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CashbackEligible).HasDefaultValue(true);
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ProductName).HasMaxLength(255);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__OrderItem__Order__06CD04F7");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.PaymentId).HasName("PK__Payments__9B556A38BC86BF79");

            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PaymentDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.User).WithMany(p => p.Payments)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payments__UserId__628FA481");
        });

        modelBuilder.Entity<Referral>(entity =>
        {
            entity.HasKey(e => e.ReferralId).HasName("PK__Referral__A2C4A966D70DD925");

            entity.Property(e => e.ReferralDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.RewardAmount)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");

            entity.HasOne(d => d.ReferredUser).WithMany(p => p.ReferralReferredUsers)
                .HasForeignKey(d => d.ReferredUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Referrals__Refer__6E01572D");

            entity.HasOne(d => d.ReferrerUser).WithMany(p => p.ReferralReferrerUsers)
                .HasForeignKey(d => d.ReferrerUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Referrals__Refer__6D0D32F4");
        });

        modelBuilder.Entity<Retailer>(entity =>
        {
            entity.HasKey(e => e.RetailerId).HasName("PK__Retailer__91152A4773F7E25C");

            entity.Property(e => e.AffiliateNetwork).HasMaxLength(100);
            entity.Property(e => e.BaseUrl).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<RetailerCategory>(entity =>
        {
            entity.HasNoKey();

            entity.HasOne(d => d.Category).WithMany()
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RetailerC__Categ__19DFD96B");

            entity.HasOne(d => d.Retailer).WithMany()
                .HasForeignKey(d => d.RetailerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RetailerC__Retai__18EBB532");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07348111E3");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053438D17A55").IsUnique();

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role).HasMaxLength(20);
            entity.Property(e => e.TotalCashback)
                .HasDefaultValue(0.00m)
                .HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<UserClick>(entity =>
        {
            entity.HasKey(e => e.ClickId).HasName("PK__UserClic__F8E74DCE8F6D1E17");

            entity.Property(e => e.ClickTime)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Link).WithMany(p => p.UserClicks)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserClick__LinkI__2645B050");

            entity.HasOne(d => d.User).WithMany(p => p.UserClicks)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserClick__UserI__25518C17");
        });

        modelBuilder.Entity<UserPurchase>(entity =>
        {
            entity.HasKey(e => e.PurchaseId).HasName("PK__UserPurc__6B0A6BBE7541A579");

            entity.Property(e => e.CashbackEarned).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PurchaseAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.PurchaseTime)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Pending");

            entity.HasOne(d => d.Link).WithMany(p => p.UserPurchases)
                .HasForeignKey(d => d.LinkId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserPurch__LinkI__2BFE89A6");

            entity.HasOne(d => d.User).WithMany(p => p.UserPurchases)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserPurch__UserI__2B0A656D");
        });

        modelBuilder.Entity<UserRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__UserRati__FCCDF87CF608B427");

            entity.Property(e => e.Comment).HasMaxLength(500);
            entity.Property(e => e.RatingDate)
                .HasDefaultValueSql("(getutcdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.User).WithMany(p => p.UserRatings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UserRatin__UserI__72C60C4A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
