using ASP_MVC_App.Data.Repositories.Interfaces;
using ASP_MVC_App.Entities;

namespace ASP_MVC_App.Data.Repositories;

public class SubjectRepository : BaseRepository, ISubjectRepository
{
    private IQueryable<Subject> AllSubjects => DbContext.Subjects;
    
    public SubjectRepository(BPContext dbContext) : base(dbContext)
    {
    }
    
    public Subject LoadById(int id)
    {
        return AllSubjects.First(s => s.Id == id);
    }

    public IEnumerable<Subject> ListSubjects()
    {
        var subjects = AllSubjects.ToList();

        return subjects.ToList();
    }
}