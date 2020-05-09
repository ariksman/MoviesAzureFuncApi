using System.Net.Http;
using System.Threading.Tasks;

namespace MoviesApi.Common
{
  public static class FinnkinoApi
  {
    private static async Task<HttpResponseMessage> GetAllAreas()
    {
      var url = "http://www.finnkino.fi/xml/TheatreAreas";
      using var client = new HttpClient();
      return await client.GetAsync(url);
    }

    private static async Task<HttpResponseMessage> GetAllMoviesInArea(int areaId)
    {
      var url = "http://www.finnkino.fi/xml/Events/?area=" + areaId;
      using var client = new HttpClient();
      return await client.GetAsync(url);
    }

    public static async Task<HttpResponseMessage> GetAllCurrentMovies()
    {
      var url = "http://www.finnkino.fi/xml/Events/";
      using var client = new HttpClient();
      return await client.GetAsync(url);
    }

    public static async Task<HttpResponseMessage> GetAllFutureMovies()
    {
      var url = "http://www.finnkino.fi/xml/Events/?listType=ComingSoon";
      using var client = new HttpClient();
      return await client.GetAsync(url);
    }
  }
}