using ASP_MVC_App.Data.Repositories.Interfaces;

namespace ASP_MVC_App.Data.Repositories;

public class RepositoryManager :  BaseRepository, IRepositoryManager
{
  private new readonly BPContext _dbContext;

  public RepositoryManager(BPContext dbContext) : base(dbContext)
  {
    _dbContext = dbContext;

    Subjects = new SubjectRepository(dbContext);
    
  }

  public void Dispose()
  {
    base.Dispose();
  }

  public ISubjectRepository Subjects { get; }
  

  public void RemoveRange(IEnumerable<object> entities)
  {
    _dbContext.RemoveRange(entities);
  }
}