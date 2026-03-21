using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Grade;

namespace NTier.Logic.Services.Interfaces;

public interface IGradeService
{
    Task<GenericResultSet<GradeResultSet>> AddSingleGrade(string name, int gradeNumber, int capacity);

    Task<GenericResultSet<List<GradeResultSet>>> GetAllGrades();

    Task<GenericResultSet<GradeResultSet>> UpdateGrade(long id, string name, int gradeNumber, int capacity);
}
