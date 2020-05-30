using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using AzureFunctionSqlApi.DataAccess;
using AzureFunctionSqlApi.DataAccess.Extensions;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MoviesApi.Common;

namespace AzureFunctionSqlApi.Functions
{
public class FinnkinoMovieQueryFunction
  {
    private readonly ILogger _logger;
    private readonly MovieRepository _movieRepository;

    public FinnkinoMovieQueryFunction(ILogger<FinnkinoMovieQueryFunction> logger,
      MovieRepository movieRepository)
    {
      _logger = logger;
      _movieRepository = movieRepository;
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

      await _movieRepository.AddOrUpdateMovies(movies);
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