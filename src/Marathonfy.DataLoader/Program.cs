using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Marathonfy.Data.Models;
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

        // Add logging
        services.AddLogging(logging =>
        {
            logging.AddConsole();
            logging.AddDebug();
        });
    })
    .Build();

Console.WriteLine("Marathonfy Data Loader");
Console.WriteLine("======================");

// Get DbContext instance
using (var scope = host.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<MarathonfyDbContext>();
    
    try
    {
        // Test database connection
        var canConnect = await dbContext.Database.CanConnectAsync();
        if (canConnect)
        {
            Console.WriteLine("✓ Database connection successful!");
            Console.WriteLine($"  Database: DBMarathonfyTest");
        }
        else
        {
            Console.WriteLine("✗ Cannot connect to database");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"✗ Database error: {ex.Message}");
    }
}

Console.WriteLine("\nApplication ready. Add your data loading logic here.");

