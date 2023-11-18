using BP.Common.Enums;

namespace BP.Common.Data.Entities;

public class UserSubject
{
    public int UserId { get; set; }
    public int SubjectId { get; set; }
    public SubjectRole SubjectRole { get; set; }
    
    public virtual User User { get; set; }
    public virtual Subject Subject { get; set; }
}