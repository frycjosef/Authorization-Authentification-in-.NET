using ASP_MVC_App.Enums;

namespace ASP_MVC_App.Entities;

public class Subject
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Semester? Semester { get; set; }
}