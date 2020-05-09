using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureFunctionSqlApi.DataAccess
{
  public class Area
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public int AreaId{ get; set; }
    [Required]
    [MaxLength(255)]
    public string Name { get; set; }

    public ICollection<ShowEvent> ShowEvents { get; set; }
  }
}