using ASP_MVC_App.Data.Repositories.Interfaces;

namespace ASP_MVC_App.Data.Repositories;

public abstract class BaseRepository : IBaseRepository, IDisposable
{
  public BaseRepository(BPContext dbContext)
  {
    _dbContext = dbContext;
  }

  public BPContext _dbContext { get; set; }

  public void SaveChanges()
  {
    _dbContext.SaveChanges();
  }

  public void Add<T>(T obj) where T : class
  {
    _dbContext.Set<T>().Add(obj);
  }

  public void Attach<T>(T obj) where T : class
  {
    _dbContext.Set<T>().Attach(obj);
  }

  public void Delete<T>(T obj) where T : class
  {
    _dbContext.Set<T>().Remove(obj);
  }

  public void Dispose()
  {
    _dbContext.Dispose();
  }
}