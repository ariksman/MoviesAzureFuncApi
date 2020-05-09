using System;
using System.IO;
using AzureFunctionSqlApi;
using AzureFunctionSqlApi.DataAccess;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AzureFunctionSqlApi
{
  public class Startup : FunctionsStartup
  {

    public override void Configure(IFunctionsHostBuilder builder)
    {
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appSettings.json", optional: true, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

      //builder.Services.Configure<AzureSqlSettings>(config.GetSection("AzureSqlSettings"));
      //builder.Services.AddOptions();

      builder.Services.AddLogging(loggingBuilder =>
      {
        loggingBuilder.AddFilter(level => true);
      });

      var connectionString = config.GetConnectionString("AzureSqlConnectionString");
      //builder.Services.AddDbContext<DatabaseContext>(
      //  options => options.UseSqlServer(connectionString));
      builder.Services.AddEfDbContextService(connectionString);
    }
  }

  public static class AddEfDbContext
  {
    private static event EventHandler InitializeDatabase = delegate { };

    public static IServiceCollection AddEfDbContextService(this IServiceCollection services, string connectionString)
    {
      if (services == null)
      {
        throw new ArgumentNullException(nameof(services));
      }

      DatabaseContext Factory(IServiceProvider sp)
      {
        //var options = sp.GetService<IOptions<AzureSqlSettings>>();
        //var azureSqlSettings = options.Value;

        if (string.IsNullOrEmpty(connectionString))
        {
          throw new ArgumentException("Please specify a valid connectionString in the local.settings.json file or your Azure Functions Settings.");
        }
        var optionsBuilder = new DbContextOptionsBuilder();
        var databaseContext = new DatabaseContext(optionsBuilder.UseSqlServer(connectionString).Options);

        async void Handler(object sender, EventArgs args)
        {
          InitializeDatabase -= Handler;
          await databaseContext.Database.MigrateAsync();
        }

        InitializeDatabase += Handler;
        InitializeDatabase(null, EventArgs.Empty); // raise event to initialize

        return databaseContext;
      }

      services.AddSingleton(Factory);
      return services;
    }
  }

  public class AzureSqlSettings
  {
    public string ConnectionString { get; set; }
  }
}