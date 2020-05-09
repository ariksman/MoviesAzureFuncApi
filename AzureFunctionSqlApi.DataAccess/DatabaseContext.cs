using Microsoft.EntityFrameworkCore;

namespace AzureFunctionSqlApi.DataAccess
{
  public class DatabaseContext : DbContext
  {
    public DatabaseContext(DbContextOptions options)
      : base(options)
    {
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<ShowEvent> ShowEvents { get; set; }
    public DbSet<Area> Areas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Movie>()
        .HasMany(m => m.ShowEvents)
        .WithOne(s => s.Movie)
        .HasForeignKey(s => s.MovieRef);

      modelBuilder.Entity<ShowEvent>()
        .HasOne(s => s.Area)
        .WithMany(a => a.ShowEvents);
    }

    //public override int SaveChanges()
    //{
    //    try
    //    {
    //      return base.SaveChanges();
    //    }
    //    catch (Exception ex)
    //    {
    //      var errorMessage = String.Empty;
    //      var token = Environment.NewLine;

    //      foreach (var entityEntry in this.ChangeTracker.Entries().Where(et => et.State != EntityState.Unchanged))
    //      {
    //        foreach (var entry in entityEntry.CurrentValues.Properties)
    //        {
    //          var result = entityEntry.GetDatabaseDefinition(entry.Name);
    //          var value = entry.PropertyInfo.GetValue(entityEntry.Entity);
    //          if (result.IsFixedLength && value.ToLength() > result.MaxLength)
    //          {
    //            errorMessage = $"{errorMessage}{token}ERROR!! <<< {result.TableName}.{result.ColumnName} {result.ColumnType.ToUpper()} :: {entry.Name}({value.ToLength()}) = {value} >>>";
    //            Log.Warning("Cannot save data to SQL column {TableName}.{ColumnName}!  Max length is {LengthTarget} and you are trying to save something that is {LengthSource}.  Column definition is {ColumnType}"
    //              , result.TableName
    //              , result.ColumnName
    //              , result.MaxLength
    //              , value.ToLength()
    //              , result.ColumnType);
    //          }
    //        }
    //      }
    //      throw new Exception(errorMessage, ex);
    //    }
    //}
  }
}