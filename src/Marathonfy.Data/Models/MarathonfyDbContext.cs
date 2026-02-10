using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Marathonfy.Data.Models;

public partial class MarathonfyDbContext : DbContext
{
    public MarathonfyDbContext()
    {
    }

    public MarathonfyDbContext(DbContextOptions<MarathonfyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<Event> Events { get; set; }

    public virtual DbSet<Race> Races { get; set; }

    public virtual DbSet<Result> Results { get; set; }

    public virtual DbSet<Stat> Stats { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=DBMarathonfyTest;Integrated Security=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("Country", "marathonfy");

            entity.Property(e => e.Acro).HasMaxLength(4);
            entity.Property(e => e.AcroIso31661Alpha2Stati)
                .HasMaxLength(2)
                .HasColumnName("Acro_iso_3166_1_alpha_2_stati");
            entity.Property(e => e.AcroIso31661Alpha3Stati)
                .HasMaxLength(3)
                .HasColumnName("Acro_iso_3166_1_alpha_3_stati");
            entity.Property(e => e.Aliases).HasMaxLength(255);
            entity.Property(e => e.Description).HasMaxLength(128);
            entity.Property(e => e.UserMod).HasMaxLength(255);
        });

        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event", "marathonfy");

            entity.Property(e => e.Country).HasMaxLength(3);
            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.UserMod).HasMaxLength(255);
            entity.Property(e => e.WebSite).HasMaxLength(255);
        });

        modelBuilder.Entity<Race>(entity =>
        {
            entity.ToTable("Race", "marathonfy");

            entity.HasIndex(e => e.EventId, "IX_Race_EventId");

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Gpx).HasColumnType("xml");
            entity.Property(e => e.UserMod).HasMaxLength(255);

            entity.HasOne(d => d.Event).WithMany(p => p.Races).HasForeignKey(d => d.EventId);
        });

        modelBuilder.Entity<Result>(entity =>
        {
            entity.ToTable("Result", "marathonfy");

            entity.HasIndex(e => e.RaceId, "IX_Result_RaceId");

            entity.Property(e => e.Athlete).HasMaxLength(255);
            entity.Property(e => e.Bib).HasMaxLength(50);
            entity.Property(e => e.BruttoTime).HasPrecision(0);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(10);
            entity.Property(e => e.DiffSplit05)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_05");
            entity.Property(e => e.DiffSplit10)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_10");
            entity.Property(e => e.DiffSplit15)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_15");
            entity.Property(e => e.DiffSplit20)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_20");
            entity.Property(e => e.DiffSplit25)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_25");
            entity.Property(e => e.DiffSplit30)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_30");
            entity.Property(e => e.DiffSplit35)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_35");
            entity.Property(e => e.DiffSplit40)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_40");
            entity.Property(e => e.DiffSplitFt)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_FT");
            entity.Property(e => e.DiffSplitHm)
                .HasPrecision(0)
                .HasColumnName("DiffSplit_HM");
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.KmH).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.KmHsplit05)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_05");
            entity.Property(e => e.KmHsplit10)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_10");
            entity.Property(e => e.KmHsplit15)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_15");
            entity.Property(e => e.KmHsplit20)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_20");
            entity.Property(e => e.KmHsplit25)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_25");
            entity.Property(e => e.KmHsplit30)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_30");
            entity.Property(e => e.KmHsplit35)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_35");
            entity.Property(e => e.KmHsplit40)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_40");
            entity.Property(e => e.KmHsplitFt)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_FT");
            entity.Property(e => e.KmHsplitHm)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("KmHSplit_HM");
            entity.Property(e => e.MinKmSplit05)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_05");
            entity.Property(e => e.MinKmSplit10)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_10");
            entity.Property(e => e.MinKmSplit15)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_15");
            entity.Property(e => e.MinKmSplit20)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_20");
            entity.Property(e => e.MinKmSplit25)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_25");
            entity.Property(e => e.MinKmSplit30)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_30");
            entity.Property(e => e.MinKmSplit35)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_35");
            entity.Property(e => e.MinKmSplit40)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_40");
            entity.Property(e => e.MinKmSplitFt)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_FT");
            entity.Property(e => e.MinKmSplitHm)
                .HasPrecision(0)
                .HasColumnName("MinKmSplit_HM");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.PaceKm).HasPrecision(0);
            entity.Property(e => e.ResultTime).HasPrecision(0);
            entity.Property(e => e.StartTime).HasPrecision(0);
            entity.Property(e => e.Surname).HasMaxLength(255);
            entity.Property(e => e.TimeDaySplit05)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_05");
            entity.Property(e => e.TimeDaySplit10)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_10");
            entity.Property(e => e.TimeDaySplit15)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_15");
            entity.Property(e => e.TimeDaySplit20)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_20");
            entity.Property(e => e.TimeDaySplit25)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_25");
            entity.Property(e => e.TimeDaySplit30)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_30");
            entity.Property(e => e.TimeDaySplit35)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_35");
            entity.Property(e => e.TimeDaySplit40)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_40");
            entity.Property(e => e.TimeDaySplitFt)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_FT");
            entity.Property(e => e.TimeDaySplitHm)
                .HasPrecision(0)
                .HasColumnName("TimeDaySplit_HM");
            entity.Property(e => e.TimeSplit05)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_05");
            entity.Property(e => e.TimeSplit10)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_10");
            entity.Property(e => e.TimeSplit15)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_15");
            entity.Property(e => e.TimeSplit20)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_20");
            entity.Property(e => e.TimeSplit25)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_25");
            entity.Property(e => e.TimeSplit30)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_30");
            entity.Property(e => e.TimeSplit35)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_35");
            entity.Property(e => e.TimeSplit40)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_40");
            entity.Property(e => e.TimeSplitFt)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_FT");
            entity.Property(e => e.TimeSplitHm)
                .HasPrecision(0)
                .HasColumnName("TimeSplit_HM");
            entity.Property(e => e.UserMod).HasMaxLength(255);

            entity.HasOne(d => d.Race).WithMany(p => p.Results).HasForeignKey(d => d.RaceId);
        });

        modelBuilder.Entity<Stat>(entity =>
        {
            entity.ToTable("Stat", "marathonfy");

            entity.HasIndex(e => e.RaceId, "IX_Stat_RaceId");

            entity.Property(e => e.AvgKmH).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AvgPaceKm).HasPrecision(0);
            entity.Property(e => e.AvgResultTime).HasPrecision(0);
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.Country).HasMaxLength(10);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.UserMod).HasMaxLength(255);

            entity.HasOne(d => d.Race).WithMany(p => p.Stats).HasForeignKey(d => d.RaceId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
