using System.Net.Http;
using System.Threading.Tasks;

namespace MoviesApi.Common
{
  public static class FinnkinoApi
  {
    private static HttpClient httpClient = new HttpClient();

    private static async Task<HttpResponseMessage> GetAllAreas()
    {
      var url = "http://www.finnkino.fi/xml/TheatreAreas";
      return await httpClient.GetAsync(url);
    }

    private static async Task<HttpResponseMessage> GetAllMoviesInArea(int areaId)
    {
      var url = "http://www.finnkino.fi/xml/Events/?area=" + areaId;
      return await httpClient.GetAsync(url);
    }

    public static async Task<HttpResponseMessage> GetAllCurrentMovies()
    {
      var url = "http://www.finnkino.fi/xml/Events/";
      return await httpClient.GetAsync(url);
    }

    public static async Task<HttpResponseMessage> GetAllFutureMovies()
    {
      var url = "http://www.finnkino.fi/xml/Events/?listType=ComingSoon";
      return await httpClient.GetAsync(url);
    }
  }
}