using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AzureFunctionCosmosDbApi.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Logging;

namespace AzureFunctionCosmosDbApi.DataAccess
{
  public class CosmosDbMovieService : ICosmosDbMovieService
  {
    private readonly ILogger<CosmosDbMovieService> _logger;
    private readonly Container _container;

    public CosmosDbMovieService(
      ILogger<CosmosDbMovieService> logger,
      CosmosClient dbClient,
      string databaseName,
      string containerName)
    {
      _logger = logger;
      _container = dbClient.GetContainer(databaseName, containerName);
    }

    public async Task AddItemAsync(Movie movie)
    {
      ItemResponse<Movie> newMovie = await _container.CreateItemAsync<Movie>(movie, new PartitionKey(movie.MovieId));
      _logger.LogInformation("Movie inserted");
      _logger.LogInformation($"This query cost: {newMovie.RequestCharge} RU/s");
    }

    public async Task DeleteItemAsync(string id)
    {
      await _container.DeleteItemAsync<Movie>(id, new PartitionKey(id));
    }

    public async Task<Movie> GetItemAsync(string id)
    {
      try
      {
        var response = await _container.ReadItemAsync<Movie>(id, new PartitionKey(id));
        return response.Resource;
      }
      catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
      {
        return null;
      }
    }

    public async Task<IEnumerable<Movie>> GetItemsAsync(string queryString)
    {
      // https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.cosmos.container.getitemlinqqueryable?view=azure-dotnet
      var query = _container.GetItemQueryIterator<Movie>(new QueryDefinition(queryString));
      var results = new List<Movie>();
      while (query.HasMoreResults)
      {
        var response = await query.ReadNextAsync();

        results.AddRange(response.ToList());
      }

      return results;
    }

    public async Task UpdateItemAsync(string id, Movie movie)
    {
      var movieResponse = await _container.UpsertItemAsync<Movie>(movie, new PartitionKey(id));
      _logger.LogInformation("Movie updated");
      _logger.LogInformation($"This query cost: {movieResponse.RequestCharge} RU/s");
    }
  }
}