using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class User
{
    public string UserId { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public string Name { get; set; } = null!;

    public virtual ICollection<Koi> Kois { get; set; } = new List<Koi>();

    public virtual ICollection<Measurement> Measurements { get; set; } = new List<Measurement>();

    public virtual ICollection<Pond> Ponds { get; set; } = new List<Pond>();
}
