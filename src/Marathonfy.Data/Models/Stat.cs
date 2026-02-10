using System;
using System.Collections.Generic;

namespace Marathonfy.Data.Models;

public partial class Stat
{
    public int Id { get; set; }

    public int RaceId { get; set; }

    public string Gender { get; set; } = null!;

    public string Category { get; set; } = null!;

    public string Country { get; set; } = null!;

    public TimeOnly AvgResultTime { get; set; }

    public TimeOnly AvgPaceKm { get; set; }

    public decimal AvgKmH { get; set; }

    public int TotalStat { get; set; }

    public bool FlagValidated { get; set; }

    public bool FlagAnn { get; set; }

    public DateTime DataMod { get; set; }

    public string UserMod { get; set; } = null!;

    public virtual Race Race { get; set; } = null!;
}
