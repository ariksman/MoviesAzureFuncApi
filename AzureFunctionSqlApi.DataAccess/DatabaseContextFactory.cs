using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace AzureFunctionSqlApi.DataAccess
{
  public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
  {

    public DatabaseContext CreateDbContext(string[] args)
    {
      var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();
      optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FinnkinoMovies;Integrated Security=True;");

      return new DatabaseContext(optionsBuilder.Options);
    }
  }
}