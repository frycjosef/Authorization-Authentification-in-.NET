using ASP_MVC_App.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ASP_MVC_App.Data.Repositories;

public class RepositoryManagerFactory : IRepositoryManagerFactory
{
  private readonly DbContextOptions<BPContext> _options;

  public RepositoryManagerFactory(DbContextOptions<BPContext> options)
  {
    _options = options;
  }

  public IRepositoryManager Create()
  {
    return new RepositoryManager(new BPContext(_options));
  }
}