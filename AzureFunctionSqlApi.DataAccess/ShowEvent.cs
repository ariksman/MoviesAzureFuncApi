using System;
using System.ComponentModel.DataAnnotations;

namespace AzureFunctionSqlApi.DataAccess
{
  public class ShowEvent
  {
    [Key]
    public int Id { get; set; }
    public int ShowEventId { get; set; }
    public DateTime StartingTime { get; set; }
    public Area Area { get; set; }

    public int MovieRef { get; set; }
    public Movie Movie { get; set; }
  }
}