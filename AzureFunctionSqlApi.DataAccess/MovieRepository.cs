using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AzureFunctionSqlApi.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AzureFunctionSqlApi.DataAccess
{
  public class MovieRepository
  {
    private readonly DatabaseContext _databaseContext;
    private readonly ILogger<MovieRepository> _logger;

    public MovieRepository(DatabaseContext databaseContext, ILogger<MovieRepository> logger)
    {
      _databaseContext = databaseContext;
      _logger = logger;
    }

    public Task<List<Movie>> GetAllMovies()
    {
      return _databaseContext.Movies.ToListAsync();
    }

    public async Task AddOrUpdateMovies(List<Movie> movies)
    {
      var moviesToBeAdded = new List<Movie>();

      foreach (var movie in movies)
      {
        try
        {
          var existingMovie = await _databaseContext.Movies.FirstOrDefaultAsync(m => m.MovieId == movie.MovieId);

          if (existingMovie == null)
          {
            _databaseContext.TruncateStringsBasedOnMaxLength(movie, _logger);
            moviesToBeAdded.Add(movie);
            continue;
          }

          existingMovie.Name = movie.Name;
          existingMovie.Description = movie.Description;
          existingMovie.DescriptionLong = movie.DescriptionLong;
          existingMovie.ImageUrl = movie.ImageUrl;
          existingMovie.HomepageUrl = movie.HomepageUrl;
          existingMovie.LocalRelease = movie.LocalRelease;

        }
        catch (Exception ex)
        {
          _logger.LogError($"Could not update movies. Exception thrown: {ex.Message}");
        }
      }

      try
      {
        await _databaseContext.Movies.AddRangeAsync(moviesToBeAdded);
        await _databaseContext.SaveChangesAsync();
      }
      catch (Exception ex)
      {
        _logger.LogError($"Could not add movies. Exception thrown: {ex.Message}");
      }
    }
  }
}