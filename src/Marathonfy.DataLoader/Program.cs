using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Marathonfy.Data.Models;
using Marathonfy.DataLoader.Services;
using Microsoft.EntityFrameworkCore;

// Create host with dependency injection
var host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) =>
    {
        // Load appsettings.json
        config.SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) =>
    {
        // Register DbContext
        var environment = context.HostingEnvironment.IsProduction() ? "ProductionConnection" : "TestConnection";
        var connectionString = context.Configuration.GetConnectionString(environment);
        services.AddDbContext<MarathonfyDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Register services
        services.AddSingleton<MarathonScraperFactory>();
        services.AddScoped<MarathonResultRepository>();
        services.AddScoped<InteractiveMenu>();

        // Add logging
        services.AddLogging(logging =>
        {
            logging.AddConsole();
            logging.AddDebug();
        });
    })
    .Build();

Console.WriteLine("╔════════════════════════════════════════╗");
Console.WriteLine("║   Marathonfy Data Loader v1.0          ║");
Console.WriteLine("║   Marathon Results Scraper & Importer  ║");
Console.WriteLine("╚════════════════════════════════════════╝\n");

// Verify database connection
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MarathonfyDbContext>();
    
    try
    {
        var canConnect = await dbContext.Database.CanConnectAsync();
        if (canConnect)
        {
            Console.WriteLine("✓ Database connection successful!\n");
        }
        else
        {
            Console.WriteLine("✗ Cannot connect to database\n");
            return;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Database error: {ex.Message}\n");
        return;
    }
}

// Run interactive menu
var cts = new CancellationTokenSource();
Console.CancelKeyPress += (s, e) =>
{
    e.Cancel = true;
    cts.Cancel();
};

using (var scope = host.Services.CreateScope())
{
    var menu = scope.ServiceProvider.GetRequiredService<InteractiveMenu>();
    await menu.RunAsync(cts.Token);
}


