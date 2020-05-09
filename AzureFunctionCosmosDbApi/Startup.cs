using System.IO;
using AzureFunctionCosmosDbApi;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

[assembly: FunctionsStartup(typeof(Startup))]
namespace AzureFunctionCosmosDbApi
{
  public class Startup : FunctionsStartup
  {

    public override void Configure(IFunctionsHostBuilder builder)
    {
      var config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appSettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .Build();

      builder.Services.Configure<CosmosDbClientSettings>(config.GetSection("CosmosDbClientSettings"));
      builder.Services.AddOptions();

      builder.Services.AddLogging(loggingBuilder =>
      {
        loggingBuilder.AddFilter(level => true);
      });

      // use options or local.settings.json for general settings in real environment
      // https://docs.microsoft.com/en-us/azure/azure-functions/functions-dotnet-dependency-injection
      builder.Services.AddCosmosDbMovieService();
    }
  }
}