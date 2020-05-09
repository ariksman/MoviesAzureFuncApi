using Microsoft.Extensions.Logging;

namespace AzureFunctionSqlApi.DataAccess.Extensions
{
  public static class StringExtensions
  {
    public static string Truncate(this string s, int length, ILogger logger = null)
    {
      string result;

      if (string.IsNullOrEmpty(s) || s.Length <= length)
      {
        result = s;
      }
      else
      {
        if (length <= 0)
        {
          result = string.Empty;
        }
        else
        {
          result = s.Substring(0, length);

          logger?.LogWarning("Truncated string:{stringToTruncate}", s);
        }
      }

      return result;
    }
  }
}