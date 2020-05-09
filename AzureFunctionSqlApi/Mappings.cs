using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using AzureFunctionSqlApi.DataAccess;

namespace AzureFunctionSqlApi
{
  public static class Mappings
  {
    public static List<Movie> ToMovies(this DataSet dt)
    {
      var imageUrls = dt.Tables[1].AsEnumerable()
        .Select(row => Convert.ToString(row["EventMediumImagePortrait"])).ToList();

      var convertedList = dt.Tables[0].AsEnumerable()
        .Select(row =>
          new Movie()
          {
            MovieId = Convert.ToString(row["ID"]),
            Name = Convert.ToString(row["OriginalTitle"]),
            Description = Convert.ToString(row["ShortSynopsis"]),
            DescriptionLong = Convert.ToString(row["Synopsis"]),
            LocalRelease = Convert.ToDateTime(row["dtLocalRelease"]),
            HomepageUrl = Convert.ToString(row["EventURL"]),
            Duration = Convert.ToDouble(row["LengthInMinutes"]),
          })
        .ToList();

      for (var i = 0; i < imageUrls.Count; i++)
      {
        convertedList[i].ImageUrl = imageUrls[i];
      }

      return convertedList;
    }
  }
}