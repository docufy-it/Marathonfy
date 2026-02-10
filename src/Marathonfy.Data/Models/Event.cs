using System;
using System.Collections.Generic;

namespace Marathonfy.Data.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string WebSite { get; set; } = null!;

    public string Country { get; set; } = null!;

    public bool FlagAnn { get; set; }

    public DateTime DataMod { get; set; }

    public string UserMod { get; set; } = null!;

    public virtual ICollection<Race> Races { get; set; } = new List<Race>();
}
