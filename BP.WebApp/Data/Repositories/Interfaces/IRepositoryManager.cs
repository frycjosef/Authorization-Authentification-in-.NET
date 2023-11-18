namespace BP.WebApp.Data.Repositories.Interfaces;

public interface IRepositoryManager : IDisposable
{
  ISubjectRepository Subjects { get; }

  void SaveChanges();
  void RemoveRange(IEnumerable<object> entities);
}