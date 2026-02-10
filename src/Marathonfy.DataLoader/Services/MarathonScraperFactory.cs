using Marathonfy.DataLoader.Interfaces;
using Marathonfy.DataLoader.Scrapers;

namespace Marathonfy.DataLoader.Services;

public class MarathonScraperFactory
{
    private readonly Dictionary<int, IMarathonScraper> _scrapers;

    public MarathonScraperFactory()
    {
        _scrapers = new Dictionary<int, IMarathonScraper>
        {
            { 1, new BostonMarathonScraper() },
            { 2, new NewYorkMarathonScraper() },
            { 3, new LondonMarathonScraper() }
        };
    }

    public IEnumerable<IMarathonScraper> GetAvailableScrapers() => _scrapers.Values;

    public IMarathonScraper? GetScraper(int marathonId) =>
        _scrapers.TryGetValue(marathonId, out var scraper) ? scraper : null;

    public void RegisterScraper(int marathonId, IMarathonScraper scraper)
    {
        _scrapers[marathonId] = scraper;
    }
}
