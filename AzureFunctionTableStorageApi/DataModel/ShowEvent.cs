using System;

namespace AzureFunctionTableStorageApi.DataModel
{
  public class ShowEvent
  {
    public DateTime StartingTime { get; set; }
    public string Location { get; set; }
  }
}