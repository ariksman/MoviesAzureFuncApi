using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AzureFunctionSqlApi.DataAccess
{
  public class Movie
  {
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(255)]
    public string MovieId { get; set; }
    [MaxLength(255)]
    public string Name { get; set; }
    [MaxLength(150)]
    public string Rating { get; set; }
    public bool InSchedule { get; set; } = true;
    [MaxLength(3000)]
    public string Description { get; set; }
    [MaxLength(3500)]
    public string DescriptionLong { get; set; }
    public DateTime LocalRelease { get; set; }
    public double Duration { get; set; }
    [MaxLength(255)]
    public string ImageUrl { get; set; }
    [MaxLength(255)]
    public string HomepageUrl { get; set; }

    public ICollection<ShowEvent> ShowEvents { get; set; }
  }
}
