namespace BP.Common.Data.Entities;

public class Subject
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? StartsAt { get; set; }
    public DateTime? EndsAt { get; set; }
    
    public List<UserSubject> UserSubjects { get; set; }
}