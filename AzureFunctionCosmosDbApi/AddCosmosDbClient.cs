using System;
using AzureFunctionCosmosDbApi.DataAccess;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureFunctionCosmosDbApi
{
  public static class AddCosmosDbClient
  {
    private static event EventHandler InitializeDatabase = delegate { };

    public static IServiceCollection AddCosmosDbMovieService(this IServiceCollection services)
    {
      if (services == null)
      {
        throw new ArgumentNullException(nameof(services));
      }

      ICosmosDbMovieService Factory(IServiceProvider sp)
      {
        var logger = sp.GetService<ILogger<CosmosDbMovieService>>();
        var options = sp.GetService<IOptions<CosmosDbClientSettings>>();
        var cosmosSettings = options.Value;

        if (string.IsNullOrEmpty(cosmosSettings.EndPointUrl))
        {
          throw new ArgumentException("Please specify a valid endpoint in the appSettings.json file or your Azure Functions Settings.");
        }

        if (string.IsNullOrEmpty(cosmosSettings.AuthorizationKey))
        {
          throw new ArgumentException("Please specify a valid AuthorizationKey in the appSettings.json file or your Azure Functions Settings.");
        }

        var clientBuilder = new CosmosClientBuilder(cosmosSettings.EndPointUrl, cosmosSettings.AuthorizationKey);
        var client = clientBuilder.Build();
        var cosmosDbMovieService = new CosmosDbMovieService(logger, client, cosmosSettings.DatabaseName, cosmosSettings.ContainerName);

        async void Handler(object sender, EventArgs args)
        {
          InitializeDatabase -= Handler;
          var databaseResponse = await client.CreateDatabaseIfNotExistsAsync(cosmosSettings.DatabaseName);
          await databaseResponse.Database.CreateContainerIfNotExistsAsync(cosmosSettings.ContainerName, "/MovieId");
        }

        InitializeDatabase += Handler;
        InitializeDatabase(null, EventArgs.Empty); // raise event to initialize

        return cosmosDbMovieService;
      }

      services.AddSingleton(Factory);
      return services;
    }
  }
}