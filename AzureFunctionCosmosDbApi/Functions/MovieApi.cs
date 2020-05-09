using System;
using System.Net;
using System.Threading.Tasks;
using AzureFunctionsCosmosDbApi.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionCosmosDbApi.Functions
{
  public class MovieApi
  {
    private readonly ILogger _logger;
    private readonly ICosmosDbMovieService _cosmosMovieService;


    public MovieApi(
      ILogger<MovieApi> logger, ICosmosDbMovieService cosmosMovieService
    )
    {
      _logger = logger;
      _cosmosMovieService = cosmosMovieService;
    }

    [FunctionName("GetMovies")]
    public async Task<IActionResult> GetMovies(
      [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movie")] HttpRequest req)
    {
      IActionResult returnValue = null;
      _logger.LogInformation("Get movies request received");

      try
      {
        var movies = await _cosmosMovieService.GetItemsAsync("SELECT * FROM c");
        returnValue = new OkObjectResult(movies);
      }
      catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests) //429
      {
        _logger.LogError($"Cant create document. Request was throttled. Exception thrown: {ex.Message}");
      }
      catch (Exception ex)
      {
        _logger.LogError($"Could not retrieve movies. Exception thrown: {ex.Message}");
        returnValue = new StatusCodeResult(StatusCodes.Status500InternalServerError);
      }

      return returnValue;
    }
  }
}