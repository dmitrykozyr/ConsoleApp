using NTier.DataAccess.Entities;
using NTier.DataAccess.Functions.CRUD_;
using NTier.DataAccess.Functions.Interfaces;
using NTier.Logic.Services.Interfaces;
using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Grade;

namespace NTier.Logic.Services.Implementation;

public class GradeService : IGradeService
{
    private ICRUD _crud = new CRUD();

    public async Task<GenericResultSet<GradeResultSet>> AddSingleGrade(string name, int gradeNumber, int capacity)
    {
        var result = new GenericResultSet<GradeResultSet>();
        
        try
        {
            var Grade = new Grade
            {
                GradeName       = name,
                GradeNumber     = gradeNumber,
                GradeCapacity   = capacity
            };

            Grade = await _crud.Create(Grade);

            var gradeAdded = new GradeResultSet
            {
                Id          = Grade.GradeId,
                Name        = Grade.GradeName,
                GradeNumber = Grade.GradeNumber,
                Capacity    = Grade.GradeCapacity
            };

            result.UserMessage = string.Format("Grade {0} was added successfully", name);
            
            result.InternalMessage = "LOGIC.Services.Implementation.Grade_Service: AddSingleGrade() method executed successfully";
            
            result.ResultSet = gradeAdded;
            
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            
            result.UserMessage = "we failed to register your information for the grade supplied. Try again";
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Grade_Service: AddSingleGrade(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<GenericResultSet<List<GradeResultSet>>> GetAllGrades()
    {
        var result = new GenericResultSet<List<GradeResultSet>>();

        try
        {
            List<Grade> Grades = await _crud.ReadAll<Grade>();
            
            result.ResultSet = new List<GradeResultSet>();
            
            Grades.ForEach(z =>
            {
                result.ResultSet.Add(new GradeResultSet
                {
                    Id          = z.GradeId,
                    Name        = z.GradeName,
                    GradeNumber = z.GradeNumber,
                    Capacity    = z.GradeCapacity
                });
            });
            
            result.UserMessage = string.Format("All grades obtained successfully");
            
            result.InternalMessage = "LOGIC.Services.Implementation.Grade_Service: GetAllGrades() method executed successfully";
            
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            
            result.UserMessage = "Failed to fetch all required grades from the DB";
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Grade_Service: GetAllGrades(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<GenericResultSet<GradeResultSet>> UpdateGrade(long id, string name, int grade_number, int capacity)
    {
        var result = new GenericResultSet<GradeResultSet>();

        try
        {
            var Grade = new Grade
            {
                GradeId             = id,
                GradeName           = name,
                GradeNumber         = grade_number,
                GradeCapacity       = capacity,
                GradeModifiedDate   = DateTime.UtcNow
            };

            Grade = await _crud.Update(Grade, id);

            var gradeUpdated = new GradeResultSet
            {
                Id          = Grade.GradeId,
                Name        = Grade.GradeName,
                GradeNumber = Grade.GradeNumber,
                Capacity    = Grade.GradeCapacity
            };

            result.UserMessage = string.Format("The supplied grade {0} was updated successfully", name);
            
            result.InternalMessage = "LOGIC.Services.Implementation.Grade_Service: UpdateGrade() method executed successfully.";
            
            result.ResultSet = gradeUpdated;
            
            result.Success = true;
        }
        catch (Exception ex)
        {
            result.Exception = ex;
            
            result.UserMessage = "Failed to update grade information";
            
            result.InternalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Grade_Service: UpdateGrade(): {0}", ex.Message);
        }

        return result;
    }
}
