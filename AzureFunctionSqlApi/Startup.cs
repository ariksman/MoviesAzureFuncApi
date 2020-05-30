using System.IO;
using AzureFunctionSqlApi;
using AzureFunctionSqlApi.DataAccess;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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

      builder.Services.AddLogging(loggingBuilder => { loggingBuilder.AddFilter(level => true); });

      builder.Services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo {Title = "My API", Version = "v1"}));

      var connectionString = config.GetConnectionString("AzureSqlConnectionString");
      builder.Services.AddDbContext(connectionString);
      builder.Services.AddSingleton(sp =>
        new MovieRepository(sp.GetService<DatabaseContext>(), sp.GetService<ILogger<MovieRepository>>()));
    }
  }

  public class AzureSqlSettings
  {
    public string ConnectionString { get; set; }
  }
}