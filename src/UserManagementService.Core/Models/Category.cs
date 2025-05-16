using System;
using System.Collections.Generic;

namespace UserManagementService.Core.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? ImageUrl { get; set; }
}
