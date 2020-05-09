using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace AzureFunctionCosmosDbApi.Domain
{
  public class Movie
  {
    public Movie(Guid id)
    {
      Id = id;
    }

    [JsonProperty(PropertyName = "id")]
    public Guid Id { get; set; }
    public string MovieId { get; set; }
    public string Name { get; set; }
    public string Rating { get; set; }
    public bool InSchedule { get; set; } = true;
    public string Description { get; set; }
    public string DescriptionLong { get; set; }
    public DateTime dtLocalRelease { get; set; }
    public double Duration { get; set; }
    public string ImageUrl { get; set; }
    public string HomepageUrl { get; set; }
    public List<ShowEvent> ShowEvents { get; set; }
  }
}
