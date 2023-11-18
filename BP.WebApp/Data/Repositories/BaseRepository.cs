using BP.WebApp.Data.Repositories.Interfaces;
using BP.Common.Data;

namespace BP.WebApp.Data.Repositories;

public abstract class BaseRepository : IBaseRepository
{
  public BaseRepository(BPContext dbContext)
  {
    DbContext = dbContext;
  }

  public BPContext DbContext { get; set; }

  public void SaveChanges()
  {
    DbContext.SaveChanges();
  }

  public void Add<T>(T obj) where T : class
  {
    DbContext.Set<T>().Add(obj);
  }

  public void Attach<T>(T obj) where T : class
  {
    DbContext.Set<T>().Attach(obj);
  }

  public void Delete<T>(T obj) where T : class
  {
    DbContext.Set<T>().Remove(obj);
  }

  public void Dispose()
  {
    DbContext.Dispose();
  }
}