using System;
using System.Collections.Generic;

namespace Marathonfy.Data.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Description { get; set; } = null!;

    public string Acro { get; set; } = null!;

    public string AcroIso31661Alpha3Stati { get; set; } = null!;

    public string AcroIso31661Alpha2Stati { get; set; } = null!;

    public string Aliases { get; set; } = null!;

    public bool FlagAnn { get; set; }

    public DateTime DataMod { get; set; }

    public string UserMod { get; set; } = null!;
}
