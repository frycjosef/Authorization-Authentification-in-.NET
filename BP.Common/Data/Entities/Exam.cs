using BP.Common.Enums;

namespace BP.Common.Data.Entities;

public class Exam
{
    public int Id { get; set; }
    public int SubjectId { get; set; }
    public Grade Grade { get; set; }
    public DateTime Date { get; set; }
    public int ExaminerId { get; set; }
    public int StudentId { get; set; }
    
    public virtual Subject Subject { get; set; }
    public virtual User Examiner { get; set; }
    public virtual User Student { get; set; }
}