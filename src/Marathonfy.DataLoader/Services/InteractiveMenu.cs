using Marathonfy.DataLoader.Services;
using Microsoft.Extensions.Logging;

namespace Marathonfy.DataLoader.Services;

public class InteractiveMenu
{
    private readonly MarathonScraperFactory _factory;
    private readonly MarathonResultRepository _repository;
    private readonly ILogger<InteractiveMenu> _logger;

    public InteractiveMenu(
        MarathonScraperFactory factory,
        MarathonResultRepository repository,
        ILogger<InteractiveMenu> logger)
    {
        _factory = factory;
        _repository = repository;
        _logger = logger;
    }

    public async Task RunAsync(CancellationToken cancellationToken = default)
    {
        while (true)
        {
            DisplayMainMenu();
            var choice = Console.ReadLine()?.Trim();

            if (choice == "0")
            {
                Console.WriteLine("\nGoodbye!");
                break;
            }

            if (choice == "1")
            {
                await ScrapeMarathonAsync(cancellationToken);
            }
            else if (choice == "2")
            {
                DisplayAvailableMarathons();
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.\n");
            }
        }
    }

    private void DisplayMainMenu()
    {
        Console.WriteLine("\n" + new string('=', 50));
        Console.WriteLine("Marathonfy Data Loader");
        Console.WriteLine(new string('=', 50));
        Console.WriteLine("1. Scrape Marathon Results");
        Console.WriteLine("2. List Available Marathons");
        Console.WriteLine("0. Exit");
        Console.Write("\nSelect option: ");
    }

    private void DisplayAvailableMarathons()
    {
        Console.WriteLine("\n" + new string('-', 50));
        Console.WriteLine("Available Marathons:");
        Console.WriteLine(new string('-', 50));

        var marathons = _factory.GetAvailableScrapers().ToList();
        for (int i = 0; i < marathons.Count; i++)
        {
            var marathon = marathons[i];
            var years = string.Join(", ", marathon.SupportedYears.OrderBy(y => y));
            Console.WriteLine($"{i + 1}. {marathon.MarathonName}");
            Console.WriteLine($"   Years: {years}");
        }
        Console.WriteLine();
    }

    private async Task ScrapeMarathonAsync(CancellationToken cancellationToken)
    {
        var marathons = _factory.GetAvailableScrapers().ToList();

        if (!marathons.Any())
        {
            Console.WriteLine("\nNo marathons available for scraping.");
            return;
        }

        Console.WriteLine("\n" + new string('-', 50));
        Console.WriteLine("Select Marathon:");
        Console.WriteLine(new string('-', 50));

        for (int i = 0; i < marathons.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {marathons[i].MarathonName}");
        }

        Console.Write("\nSelect marathon (number): ");
        if (!int.TryParse(Console.ReadLine()?.Trim(), out var marathonChoice) ||
            marathonChoice < 1 || marathonChoice > marathons.Count)
        {
            Console.WriteLine("Invalid selection.");
            return;
        }

        var selectedMarathon = marathons[marathonChoice - 1];
        Console.WriteLine($"\nSelected: {selectedMarathon.MarathonName}");

        var supportedYears = selectedMarathon.SupportedYears.OrderBy(y => y).ToList();
        Console.WriteLine($"\nAvailable years: {string.Join(", ", supportedYears)}");
        Console.Write("Enter year: ");

        if (!int.TryParse(Console.ReadLine()?.Trim(), out var yearChoice) ||
            !supportedYears.Contains(yearChoice))
        {
            Console.WriteLine("Invalid year selection.");
            return;
        }

        Console.WriteLine($"\nScraping {selectedMarathon.MarathonName} for {yearChoice}...");

        try
        {
            // Scrape results
            var results = await selectedMarathon.ScrapeAsync(yearChoice, cancellationToken);
            Console.WriteLine($"Retrieved {results.Count} results.");

            if (results.Count == 0)
            {
                Console.WriteLine("No results found.");
                return;
            }

            // Get or create race record
            var race = await _repository.GetOrCreateRaceAsync(
                selectedMarathon.EventId,
                yearChoice,
                cancellationToken);

            if (race == null)
            {
                Console.WriteLine("Error creating race record.");
                return;
            }

            // Save results
            var savedCount = await _repository.SaveResultsAsync(race.Id, results, cancellationToken);
            Console.WriteLine($"\n✓ Successfully saved {savedCount} results to database.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during scraping process");
            Console.WriteLine($"\n✗ Error: {ex.Message}");
        }
    }
}
