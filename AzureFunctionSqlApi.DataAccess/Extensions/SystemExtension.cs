using Newtonsoft.Json;

namespace AzureFunctionSqlApi.DataAccess.Extensions
{
  public static class SystemExtension
  {
    public static T Clone<T>(this T source)
    {
      var serialized = JsonConvert.SerializeObject(source);
      return JsonConvert.DeserializeObject<T>(serialized);
    }
  }
}