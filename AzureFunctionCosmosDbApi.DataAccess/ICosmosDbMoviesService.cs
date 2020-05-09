using System.Collections.Generic;
using System.Threading.Tasks;
using AzureFunctionCosmosDbApi.Domain;

namespace AzureFunctionCosmosDbApi.DataAccess
{
  public interface ICosmosDbMovieService   
  {
    Task<IEnumerable<Movie>> GetItemsAsync(string query);
    Task<Movie> GetItemAsync(string id);
    Task AddItemAsync(Movie movie);
    Task UpdateItemAsync(string id, Movie item);
    Task DeleteItemAsync(string id);
  }
}