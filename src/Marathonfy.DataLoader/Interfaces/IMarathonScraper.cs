namespace Marathonfy.DataLoader.Interfaces;

public interface IMarathonScraper
{
    /// <summary>
    /// Gets the name of the marathon
    /// </summary>
    string MarathonName { get; }

    /// <summary>
    /// Gets the event ID in the database
    /// </summary>
    int EventId { get; }

    /// <summary>
    /// Gets the list of supported years for scraping
    /// </summary>
    IEnumerable<int> SupportedYears { get; }

    /// <summary>
    /// Scrapes the marathon results for a given year
    /// </summary>
    Task<List<MarathonResultDto>> ScrapeAsync(int year, CancellationToken cancellationToken = default);
}

public class MarathonResultDto
{
    public string AthleteName { get; set; } = string.Empty;
    public TimeSpan FinishTime { get; set; }
    public string BibNumber { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public int? Age { get; set; }
    public int? Placement { get; set; }
}
