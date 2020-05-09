using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using AzureFunctionTableStorageApi.DataModel;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage.Table;
using MoviesApi.Common;

namespace AzureFunctionTableStorageApi
{
  public static class FinnkinoMovieQueryFunction
  {
    [FunctionName("FinnkinoMovieQueryFunction")]
    public static async Task Run([TimerTrigger("0 */90 * * * *")] TimerInfo myTimer,
      [Table("movies", Connection = "AzureWebJobsStorage")] IAsyncCollector<MovieTableEntity> movieTable,
      [Table("movies", Connection = "AzureWebJobsStorage")] CloudTable movieCloudTable,
      ILogger log)
    {
      log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
      var response = await FinnkinoApi.GetAllCurrentMovies();
      var movies = await ParseXmlToMovies(response);

      foreach (var movie in movies)
      {
        var findOperation = TableOperation.Retrieve<MovieTableEntity>("MOVIE", movie.Id);
        var findResult = await movieCloudTable.ExecuteAsync(findOperation);
        if (findResult.Result == null)
        {
          await movieTable.AddAsync(movie.ToTableEntity());
          continue;
        }

        var existingRow = (MovieTableEntity)findResult.Result;
        await UpdateProperties(movieCloudTable, existingRow, movie);
      }
    }

    public static async Task UpdateProperties(CloudTable movieCloudTable, MovieTableEntity existingRow,
      Movie updatedMovie)
    {
      existingRow.Name = updatedMovie.Name;
      existingRow.Description = updatedMovie.Description;
      existingRow.DescriptionLong = updatedMovie.DescriptionLong;
      existingRow.ImageUrl = updatedMovie.ImageUrl;
      existingRow.HomepageUrl = updatedMovie.HomepageUrl;
      existingRow.dtLocalRelease = updatedMovie.dtLocalRelease;

      var replaceOperation = TableOperation.Replace(existingRow);
      await movieCloudTable.ExecuteAsync(replaceOperation);
    }

    public static async Task<List<Movie>> ParseXmlToMovies(HttpResponseMessage response)
    {
      if (response?.StatusCode != HttpStatusCode.OK) return null;

      var responseData = await response.Content.ReadAsStringAsync();
      var xdoc = XDocument.Parse(responseData);

      var sr = new StringReader(xdoc.ToString());
      var dataSet = new DataSet();

      dataSet.ReadXml(sr);
      var movies = dataSet.ToMovies();
      return movies;
    }
  }
}