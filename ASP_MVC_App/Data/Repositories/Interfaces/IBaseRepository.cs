namespace ASP_MVC_App.Data.Repositories.Interfaces;

public interface IBaseRepository : IDisposable
{
  void SaveChanges();
  void Attach<T>(T obj) where T : class;
  void Delete<T>(T obj) where T : class;
  void Add<T>(T obj) where T : class;
}