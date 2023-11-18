using BP.Common.Data.Entities;

namespace BP.WebApp.Models;

public class DashboardModel
{
    public List<UserSubject> UserSubjects { get; set; } = new();
    
}