using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage.Table;

namespace AzureFunctionTableStorageApi.DataModel
{
  public class MovieTableEntity : TableEntity
  {
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