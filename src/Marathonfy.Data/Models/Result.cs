using System;
using System.Collections.Generic;

namespace Marathonfy.Data.Models;

public partial class Result
{
    public int Id { get; set; }

    public int RaceId { get; set; }

    public string Athlete { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Country { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly ResultTime { get; set; }

    public TimeOnly BruttoTime { get; set; }

    public TimeOnly PaceKm { get; set; }

    public decimal KmH { get; set; }

    public string Bib { get; set; } = null!;

    public string Category { get; set; } = null!;

    public int OverallRankt { get; set; }

    public int CatergoryRank { get; set; }

    public int GenderRank { get; set; }

    public TimeOnly TimeDaySplit05 { get; set; }

    public TimeOnly TimeDaySplit10 { get; set; }

    public TimeOnly TimeDaySplit15 { get; set; }

    public TimeOnly TimeDaySplit20 { get; set; }

    public TimeOnly TimeDaySplitHm { get; set; }

    public TimeOnly TimeDaySplit25 { get; set; }

    public TimeOnly TimeDaySplit30 { get; set; }

    public TimeOnly TimeDaySplit35 { get; set; }

    public TimeOnly TimeDaySplit40 { get; set; }

    public TimeOnly TimeDaySplitFt { get; set; }

    public TimeOnly TimeSplit05 { get; set; }

    public TimeOnly TimeSplit10 { get; set; }

    public TimeOnly TimeSplit15 { get; set; }

    public TimeOnly TimeSplit20 { get; set; }

    public TimeOnly TimeSplitHm { get; set; }

    public TimeOnly TimeSplit25 { get; set; }

    public TimeOnly TimeSplit30 { get; set; }

    public TimeOnly TimeSplit35 { get; set; }

    public TimeOnly TimeSplit40 { get; set; }

    public TimeOnly TimeSplitFt { get; set; }

    public TimeOnly DiffSplit05 { get; set; }

    public TimeOnly DiffSplit10 { get; set; }

    public TimeOnly DiffSplit15 { get; set; }

    public TimeOnly DiffSplit20 { get; set; }

    public TimeOnly DiffSplitHm { get; set; }

    public TimeOnly DiffSplit25 { get; set; }

    public TimeOnly DiffSplit30 { get; set; }

    public TimeOnly DiffSplit35 { get; set; }

    public TimeOnly DiffSplit40 { get; set; }

    public TimeOnly DiffSplitFt { get; set; }

    public TimeOnly MinKmSplit05 { get; set; }

    public TimeOnly MinKmSplit10 { get; set; }

    public TimeOnly MinKmSplit15 { get; set; }

    public TimeOnly MinKmSplit20 { get; set; }

    public TimeOnly MinKmSplitHm { get; set; }

    public TimeOnly MinKmSplit25 { get; set; }

    public TimeOnly MinKmSplit30 { get; set; }

    public TimeOnly MinKmSplit35 { get; set; }

    public TimeOnly MinKmSplit40 { get; set; }

    public TimeOnly MinKmSplitFt { get; set; }

    public decimal KmHsplit05 { get; set; }

    public decimal KmHsplit10 { get; set; }

    public decimal KmHsplit15 { get; set; }

    public decimal KmHsplit20 { get; set; }

    public decimal KmHsplitHm { get; set; }

    public decimal KmHsplit25 { get; set; }

    public decimal KmHsplit30 { get; set; }

    public decimal KmHsplit35 { get; set; }

    public decimal KmHsplit40 { get; set; }

    public decimal KmHsplitFt { get; set; }

    public bool FlagValidated { get; set; }

    public bool FlagAnn { get; set; }

    public DateTime DataMod { get; set; }

    public string UserMod { get; set; } = null!;

    public virtual Race Race { get; set; } = null!;
}
