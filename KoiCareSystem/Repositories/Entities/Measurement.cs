using System;
using System.Collections.Generic;

namespace Repositories.Entities;

public partial class Measurement
{
    public int MeasureId { get; set; }

    public int PondId { get; set; }

    public string UserId { get; set; } = null!;

    public double Nitrite { get; set; }

    public double Oxygen { get; set; }

    public double Nitrate { get; set; }

    public DateTime CreatedAt { get; set; }

    public double Temperature { get; set; }

    public double Phosphate { get; set; }

    public double PH { get; set; }

    public double Ammonium { get; set; }

    public double Hardness { get; set; }

    public double CarbonDioxide { get; set; }

    public double CarbonHardness { get; set; }

    public double Salt { get; set; }

    public double TotalChlorines { get; set; }

    public double OutdoorTemp { get; set; }

    public double AmountFed { get; set; }

    public virtual Pond Pond { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
