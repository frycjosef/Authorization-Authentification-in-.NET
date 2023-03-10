using ASP_MVC_App.Entities;

namespace ASP_MVC_App.Data.Repositories.Interfaces;

public interface ISubjectRepository
{
    public Subject LoadById(int id);
    public IEnumerable<Subject> ListSubjects();
}