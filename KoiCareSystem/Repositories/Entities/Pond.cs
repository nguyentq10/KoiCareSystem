using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Pond
{
    public int PondId { get; set; }

    public string UserId { get; set; } = null!;

    public string Name { get; set; } = null!;

    public int Volume { get; set; }

    public string? Thumbnail { get; set; }

    public float Depth { get; set; }

    public int PumpingCapacity { get; set; }

    public int Drain { get; set; }

    public int Skimmer { get; set; }

    public string? Note { get; set; }

    public virtual ICollection<Koi> Kois { get; set; } = new List<Koi>();

    public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();

    public virtual User User { get; set; } = null!;
}
