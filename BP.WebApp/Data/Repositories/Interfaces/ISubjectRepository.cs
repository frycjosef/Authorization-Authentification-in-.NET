using BP.Common.Data.Entities;

namespace BP.WebApp.Data.Repositories.Interfaces;

public interface ISubjectRepository : IBaseRepository
{
    public Subject LoadById(int id);
    public IEnumerable<Subject> ListSubjects();
}