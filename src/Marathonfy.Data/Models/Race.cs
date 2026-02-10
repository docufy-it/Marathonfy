using System;
using System.Collections.Generic;

namespace Marathonfy.Data.Models;

public partial class Race
{
    public int Id { get; set; }

    public int EventId { get; set; }

    public string Description { get; set; } = null!;

    public int Year { get; set; }

    public DateOnly Date { get; set; }

    public string? Gpx { get; set; }

    public bool FlagValidated { get; set; }

    public bool FlagAnn { get; set; }

    public DateTime DataMod { get; set; }

    public string UserMod { get; set; } = null!;

    public virtual Event Event { get; set; } = null!;

    public virtual ICollection<Result> Results { get; set; } = new List<Result>();

    public virtual ICollection<Stat> Stats { get; set; } = new List<Stat>();
}
