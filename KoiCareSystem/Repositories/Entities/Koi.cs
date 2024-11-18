using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Koi
{
    public int KoiId { get; set; }

    public int PondId { get; set; }

    public string UserId { get; set; } = null!;

    public int Age { get; set; }

    public string? Thumbnail { get; set; }

    public string Name { get; set; } = null!;

    public string? Note { get; set; }

    public string Origin { get; set; } = null!;

    public int Length { get; set; }

    public float Weight { get; set; }

    public string Color { get; set; } = null!;

    public DateTime CreateAt { get; set; }

    public string Sex { get; set; } = null!;

    public string Variety { get; set; } = null!;

    public string Physique { get; set; } = null!;

    public bool? Status { get; set; }

    public virtual Pond Pond { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
