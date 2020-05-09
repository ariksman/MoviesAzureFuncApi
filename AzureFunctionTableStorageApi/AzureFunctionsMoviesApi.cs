using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AzureFunctionTableStorageApi.DataModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;

namespace AzureFunctionTableStorageApi
{
  public static class AzureFunctionsMoviesApi
  {

    [FunctionName("GetMovies")]
    public static async Task<IActionResult> GetMovies(
      [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movie")] HttpRequest req,
      [Table("movies", Connection = "AzureWebJobsStorage")] CloudTable movieTable,
      ILogger log)
    {
      log.LogInformation("Get all movies");
      var query = new TableQuery<MovieTableEntity>();
      var segment = await movieTable.ExecuteQuerySegmentedAsync(query, null);
      return new OkObjectResult(segment.Select(m =>m.ToMovie()));
    }

    [FunctionName("GetMovieById")]
    public static async Task<IActionResult> GetMovieById(
      [HttpTrigger(AuthorizationLevel.Function, "get", Route = "movie/{id}")] HttpRequest req,
      [Table("movies", "MOVIE", "{id}", Connection = "AzureWebJobsStorage")] MovieTableEntity movieTable,
      ILogger log, Guid id)
    {
      log.LogInformation("Getting movie item by id");
      if (movieTable != null) return new OkObjectResult(movieTable.ToMovie());

      log.LogInformation($"Item {id} not found");
      return new NotFoundResult();
    }

    [FunctionName("CreateMovie")]
    public static async Task<IActionResult> CreateMovie(
      [HttpTrigger(AuthorizationLevel.Function, "post", Route = "movie")] HttpRequest req,
      [Table("movies", Connection = "AzureWebJobsStorage")] IAsyncCollector<MovieTableEntity> movieTable,
      ILogger log)
    {
      log.LogInformation("Create a new movie.");

      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var data = JsonConvert.DeserializeObject<MovieInput>(requestBody);

      var movie = new Movie() {Name = data.Name, Description = data.Description, Id = Guid.NewGuid().ToString()};
      await movieTable.AddAsync(movie.ToTableEntity());

      return new OkObjectResult(movie);
    }

    [FunctionName("UpdateMovie")]
    public static async Task<IActionResult> UpdateMovie(
      [HttpTrigger(AuthorizationLevel.Function, "put", Route = "movie/{id}")] HttpRequest req,
      [Table("movies", Connection = "AzureWebJobsStorage")] CloudTable movieTable,
      ILogger log, Guid id)
    {
      log.LogInformation("Update a movie.");
      string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
      var updated = JsonConvert.DeserializeObject<MovieUpdate>(requestBody);
      var findOperation = TableOperation.Retrieve<MovieTableEntity>("MOVIE", id.ToString());
      var findResult = await movieTable.ExecuteAsync(findOperation);
      if (findResult.Result == null)
      {
        return new NotFoundResult();
      }
      var existingRow = (MovieTableEntity)findResult.Result;
      existingRow.Name = updated.Name;
      if (!string.IsNullOrEmpty(updated.Description))
      {
        existingRow.Description = updated.Description;
      }

      var replaceOperation = TableOperation.Replace(existingRow);
      await movieTable.ExecuteAsync(replaceOperation);
      return new OkObjectResult(existingRow.ToMovie());
    }

    [FunctionName("DeleteMovie")]
    public static async Task<IActionResult> DeleteMovie(
      [HttpTrigger(AuthorizationLevel.Function, "delete", Route = "movie/{id}")] HttpRequest req,
      [Table("movies", Connection = "AzureWebJobsStorage")] CloudTable movieTable,
      ILogger log, Guid id)
    {
      log.LogInformation("delete a new movie.");
      var deleteOperation = TableOperation.Delete(new TableEntity()
        { PartitionKey = "MOVIE", RowKey = id.ToString(), ETag = "*" });
      try
      {
        var deleteResult = await movieTable.ExecuteAsync(deleteOperation);
      }
      catch (StorageException e) when (e.RequestInformation.HttpStatusCode == 404)
      {
        return new NotFoundResult();
      }
      return new OkResult();
    }
  }
}