using Marathonfy.Data.Models;
using Marathonfy.DataLoader.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Marathonfy.DataLoader.Services;

public class MarathonResultRepository
{
    private readonly MarathonfyDbContext _context;
    private readonly ILogger<MarathonResultRepository> _logger;

    public MarathonResultRepository(MarathonfyDbContext context, ILogger<MarathonResultRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> SaveResultsAsync(
        int raceId,
        List<MarathonResultDto> results,
        CancellationToken cancellationToken = default)
    {
        if (!results.Any())
        {
            _logger.LogWarning("No results to save for race ID {RaceId}", raceId);
            return 0;
        }

        try
        {
            var savedCount = 0;

            foreach (var resultDto in results)
            {
                // Check if result already exists
                var exists = await _context.Results
                    .AnyAsync(r => r.RaceId == raceId && r.Athlete == resultDto.AthleteName,
                        cancellationToken);

                if (!exists)
                {
                    // Parse finish time for TimeOnly
                    var resultTime = TimeOnly.FromTimeSpan(resultDto.FinishTime);

                    var result = new Result
                    {
                        RaceId = raceId,
                        Athlete = resultDto.AthleteName,
                        Bib = resultDto.BibNumber,
                        ResultTime = resultTime,
                        BruttoTime = resultTime,
                        Gender = resultDto.Gender,
                        Country = resultDto.Country,
                        Category = string.Empty,
                        Name = resultDto.AthleteName,
                        Surname = string.Empty,
                        StartTime = TimeOnly.MinValue,
                        PaceKm = TimeOnly.MinValue,
                        KmH = 0,
                        OverallRankt = resultDto.Placement ?? 0,
                        CatergoryRank = 0,
                        GenderRank = 0
                    };

                    _context.Results.Add(result);
                    savedCount++;
                }
            }

            if (savedCount > 0)
            {
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Saved {Count} marathon results for race ID {RaceId}",
                    savedCount, raceId);
            }

            return savedCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving marathon results for race ID {RaceId}", raceId);
            throw;
        }
    }

    public async Task<Race?> GetOrCreateRaceAsync(
        int eventId,
        int year,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var race = await _context.Races
                .FirstOrDefaultAsync(r => r.EventId == eventId && r.Year == year, cancellationToken);

            if (race == null)
            {
                race = new Race
                {
                    EventId = eventId,
                    Year = year,
                    Description = $"Marathon {year}",
                    UserMod = "DataLoader"
                };

                _context.Races.Add(race);
                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Created new race record for event {EventId} year {Year}",
                    eventId, year);
            }

            return race;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting or creating race for event {EventId} year {Year}",
                eventId, year);
            throw;
        }
    }
}
