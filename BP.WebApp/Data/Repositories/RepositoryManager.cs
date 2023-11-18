using BP.WebApp.Data.Repositories.Interfaces;
using BP.Common.Data;

namespace BP.WebApp.Data.Repositories;

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