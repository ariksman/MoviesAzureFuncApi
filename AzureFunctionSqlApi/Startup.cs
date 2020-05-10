using System.IO;
using AzureFunctionSqlApi;
using AzureFunctionSqlApi.DataAccess;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
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
      builder.Services.AddDbContext(connectionString);
      builder.Services.AddSingleton(sp => new MovieRepository(sp.GetService<DatabaseContext>()));
    }
  }

  public class AzureSqlSettings
  {
    public string ConnectionString { get; set; }
  }
}