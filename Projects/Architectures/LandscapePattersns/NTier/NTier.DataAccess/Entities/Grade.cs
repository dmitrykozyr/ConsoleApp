namespace NTier.DataAccess.Entities;

public class Grade
{
    public long GradeId { get; set; }

    public string? GradeName { get; set; }

    public int GradeNumber { get; set; }

    public int GradeCapacity { get; set; }

    public DateTime GradeCreationDate { get; set; }

    public DateTime GradeModifiedDate { get; set; }

    public ICollection<Application>? Applications { get; set; }
}
