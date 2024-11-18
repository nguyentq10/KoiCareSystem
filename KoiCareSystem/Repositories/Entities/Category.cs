using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<ExternalProduct> ExternalProducts { get; set; } = new List<ExternalProduct>();
}
