using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class ExternalProduct
{
    public int ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? Thumbnail { get; set; }

    public string ExternalUrl { get; set; } = null!;

    public int? CategoryId { get; set; }

    public bool Status { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Category? Category { get; set; }
}
