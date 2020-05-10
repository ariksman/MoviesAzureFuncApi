using System;
using AzureFunctionSqlApi.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionSqlApi
{
  public static class DatabaseContextExtensionMethods
  {
    private static event EventHandler InitializeDatabase = delegate { };

    public static IServiceCollection AddDbContext(this IServiceCollection services, string connectionString)
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
}