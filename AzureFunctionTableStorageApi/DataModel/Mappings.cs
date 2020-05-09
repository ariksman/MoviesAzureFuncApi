using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace AzureFunctionTableStorageApi.DataModel
{
  public static class Mappings
  {
    public static MovieTableEntity ToTableEntity(this Movie movie)
    {
      return new MovieTableEntity()
      {
        PartitionKey = "MOVIE",
        RowKey = movie.Id.ToString(),
        Name = movie.Name,
        Description = movie.Description,
      };
    }

    public static Movie ToMovie(this MovieTableEntity movie)
    {
      //Guid.TryParse(movie.RowKey, out var id);
      return new Movie(movie.RowKey)
      {
        Name = movie.Name,
        Description = movie.Description,
        DescriptionLong = movie.DescriptionLong,
        HomepageUrl = movie.HomepageUrl,
        ImageUrl = movie.ImageUrl,
      };
    }

    public static List<Movie> ToMovies(this DataSet dt)
    {
      var imageUrls = dt.Tables[1].AsEnumerable()
        .Select(row => Convert.ToString(row["EventMediumImagePortrait"])).ToList();

      var convertedList = dt.Tables[0].AsEnumerable()
        .Select(row =>
          new Movie(Convert.ToString(row["ID"]))
          {
            Name = Convert.ToString(row["OriginalTitle"]),
            Description = Convert.ToString(row["ShortSynopsis"]),
            DescriptionLong = Convert.ToString(row["Synopsis"]),
            dtLocalRelease = Convert.ToDateTime(row["dtLocalRelease"]),
            HomepageUrl = Convert.ToString(row["EventURL"]),
            Duration = Convert.ToDouble(row["LengthInMinutes"]),
          })
        .ToList();

      for (int i = 0; i < imageUrls.Count; i++)
      {
        convertedList[i].ImageUrl = imageUrls[i];
      }

      return convertedList;
    }
  }
}