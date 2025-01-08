using NTier.DataAccess.Entities;
using NTier.DataAccess.Functions.CRUD_;
using NTier.DataAccess.Functions.Interfaces;
using NTier.Logic.Services.Interfaces;
using NTier.Logic.Services.Models;
using NTier.Logic.Services.Models.Grade;

namespace NTier.Logic.Services.Implementation;

public class Grade_Service : IGrade_Service
{
    private ICRUD _crud = new CRUD();

    public async Task<Generic_ResultSet<Grade_ResultSet>> AddSingleGrade(string name, int grade_number, int capacity)
    {
        var result = new Generic_ResultSet<Grade_ResultSet>();
        try
        {
            Grade Grade = new Grade
            {
                Grade_Name = name,
                Grade_Number = grade_number,
                Grade_Capacity = capacity
            };

            Grade = await _crud.Create<Grade>(Grade);

            Grade_ResultSet gradeAdded = new Grade_ResultSet
            {
                id = Grade.Grade_ID,
                name = Grade.Grade_Name,
                grade_number = Grade.Grade_Number,
                capacity = Grade.Grade_Capacity
            };

            result.userMessage = string.Format("Grade {0} was added successfully", name);
            result.internalMessage = "LOGIC.Services.Implementation.Grade_Service: AddSingleGrade() method executed successfully";
            result.result_set = gradeAdded;
            result.success = true;
        }
        catch (Exception ex)
        {
            result.exception = ex;
            result.userMessage = "we failed to register your information for the grade supplied. Try again";
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Grade_Service: AddSingleGrade(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<Generic_ResultSet<List<Grade_ResultSet>>> GetAllGrades()
    {
        var result = new Generic_ResultSet<List<Grade_ResultSet>>();

        try
        {
            List<Grade> Grades = await _crud.ReadAll<Grade>();
            result.result_set = new List<Grade_ResultSet>();
            Grades.ForEach(z =>
            {
                result.result_set.Add(new Grade_ResultSet
                {
                    id = z.Grade_ID,
                    name = z.Grade_Name,
                    grade_number = z.Grade_Number,
                    capacity = z.Grade_Capacity
                });
            });
            
            result.userMessage = string.Format("All grades obtained successfully");
            result.internalMessage = "LOGIC.Services.Implementation.Grade_Service: GetAllGrades() method executed successfully";
            result.success = true;
        }
        catch (Exception ex)
        {
            result.exception = ex;
            result.userMessage = "Failed to fetch all required grades from the DB";
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Grade_Service: GetAllGrades(): {0}", ex.Message);
        }

        return result;
    }

    public async Task<Generic_ResultSet<Grade_ResultSet>> UpdateGrade(Int64 id, string name, int grade_number, int capacity)
    {
        var result = new Generic_ResultSet<Grade_ResultSet>();

        try
        {
            var Grade = new Grade
            {
                Grade_ID = id,
                Grade_Name = name,
                Grade_Number = grade_number,
                Grade_Capacity = capacity,
                Grade_ModifiedDate = DateTime.UtcNow
            };

            Grade = await _crud.Update<Grade>(Grade, id);

            var gradeUpdated = new Grade_ResultSet
            {
                id = Grade.Grade_ID,
                name = Grade.Grade_Name,
                grade_number = Grade.Grade_Number,
                capacity = Grade.Grade_Capacity
            };

            result.userMessage = string.Format("The supplied grade {0} was updated successfully", name);
            result.internalMessage = "LOGIC.Services.Implementation.Grade_Service: UpdateGrade() method executed successfully.";
            result.result_set = gradeUpdated;
            result.success = true;
        }
        catch (Exception ex)
        {
            result.exception = ex;
            result.userMessage = "Failed to update grade information";
            result.internalMessage = string.Format("ERROR: LOGIC.Services.Implementation.Grade_Service: UpdateGrade(): {0}", ex.Message);
        }

        return result;
    }
}
