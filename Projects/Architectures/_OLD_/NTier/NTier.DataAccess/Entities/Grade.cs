namespace NTier.DataAccess.Entities;

public class Grade
{
    public long GradeId { get; init; }

    public string? GradeName { get; init; }

    public int GradeNumber { get; init; }

    public int GradeCapacity { get; init; }

    public DateTime GradeCreationDate { get; init; }

    public DateTime GradeModifiedDate { get; init; }

    public ICollection<Application>? Applications { get; init; }
}
