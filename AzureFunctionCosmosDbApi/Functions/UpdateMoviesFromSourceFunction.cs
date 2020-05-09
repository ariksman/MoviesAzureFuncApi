using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using AzureFunctionCosmosDbApi.DataAccess;
using AzureFunctionCosmosDbApi.Domain;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using MoviesApi.Common;

namespace AzureFunctionCosmosDbApi.Functions
{

  public class FinnkinoMovieQueryFunction
  {
    private readonly ILogger _logger;
    private readonly ICosmosDbMovieService _cosmosMovieService;

    public FinnkinoMovieQueryFunction(ILogger<MovieApi> logger,
      ICosmosDbMovieService cosmosMovieService)
    {
      _logger = logger;
      _cosmosMovieService = cosmosMovieService;
    }

    [FunctionName("FinnkinoMovieQueryFunction")]
    public async Task Run([TimerTrigger("0 */90 * * * *")] TimerInfo myTimer, ILogger log)
    {
      log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
      var response = await FinnkinoApi.GetAllCurrentMovies();
      var movies = await ParseXmlToMovies(response);
      var response2 = await FinnkinoApi.GetAllFutureMovies();
      var movies2 = await ParseXmlToMovies(response2);
      foreach (var movie in movies2)
      {
        movie.InSchedule = false;
      }
      movies.AddRange(movies2);


      foreach (var movie in movies)
      {
        try
        {
          var existingMovie = _cosmosMovieService.GetItemAsync(movie.MovieId);

          if (existingMovie == null)
          {
            await _cosmosMovieService.AddItemAsync(movie);
            continue;
          }

          await _cosmosMovieService.UpdateItemAsync(movie.MovieId, movie);
        }
        catch (CosmosException ex) when (ex.StatusCode == HttpStatusCode.TooManyRequests) //429
        {
          _logger.LogError($"Cant create document. Request was throttled. Exception thrown: {ex.Message}");
        }
        catch (Exception ex)
        {
          _logger.LogError($"Could not update movies. Exception thrown: {ex.Message}");
        }
      }
    }

    public static async Task<List<Movie>> ParseXmlToMovies(HttpResponseMessage response)
    {
      if (response?.StatusCode != HttpStatusCode.OK) return null;

      var responseData = await response.Content.ReadAsStringAsync();
      var xDoc = XDocument.Parse(responseData);

      var sr = new StringReader(xDoc.ToString());
      var dataSet = new DataSet();

      dataSet.ReadXml(sr);
      var movies = dataSet.ToMovies();
      return movies;
    }
  }
}

