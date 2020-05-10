using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AzureFunctionSqlApi.DataAccess
{
  public class MovieRepository
  {
    private readonly DatabaseContext _databaseContext;

    public MovieRepository(DatabaseContext databaseContext)
    {
      _databaseContext = databaseContext;
    }

    public Task<List<Movie>> GetAllMovies()
    {
      return _databaseContext.Movies.ToListAsync();
    }
  }
}