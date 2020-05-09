using System;
using System.Collections.Generic;

namespace AzureFunctionTableStorageApi.DataModel
{
  public class Movie
  {
    public Movie()
    {
      Id = Guid.NewGuid().ToString();
    }

    public Movie(Guid id)
    {
      Id = id.ToString();
    }

    public Movie(string id)
    {
      Id = id;
    }

    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string DescriptionLong { get; set; }
    public DateTime dtLocalRelease { get; set; }
    public double Duration { get; set; }
    public string ImageUrl { get; set; }
    public string HomepageUrl { get; set; }
    public List<ShowEvent> ShowEvents { get; set; }
  }
}
