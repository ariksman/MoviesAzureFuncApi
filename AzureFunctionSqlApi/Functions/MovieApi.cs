using System;
using System.Threading.Tasks;
using AzureFunctionSqlApi.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionSqlApi.Functions
{
  public class MovieApi
  {
    private readonly ILogger _logger;
    private readonly MovieRepository _movieRepository;


    public MovieApi(
      ILogger<MovieApi> logger, MovieRepository movieRepository
    )
    {
      _logger = logger;
      _movieRepository = movieRepository;
    }

    [FunctionName("GetMovies")]
    public async Task<IActionResult> GetMovies(
      [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movie")] HttpRequest req)
    {
      IActionResult returnValue = null;
      _logger.LogInformation("Get movies request received");

      try
      {
        var movies = await _movieRepository.GetAllMovies();
        returnValue = new OkObjectResult(movies);
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