using System;

namespace AzureFunctionCosmosDbApi.Domain
{
  public class ShowEvent
  {
    public DateTime StartingTime { get; set; }
    public Area Area { get; set; }
  }
}