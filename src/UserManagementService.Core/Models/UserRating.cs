using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class UserRating
{
    public int RatingId { get; set; }

    public int UserId { get; set; }

    public byte Rating { get; set; }

    public string? Comment { get; set; }

    public DateTime? RatingDate { get; set; }

    public virtual User User { get; set; } = null!;
}
