﻿using Microsoft.EntityFrameworkCore;
using NTier.DataAccess.DataContext;
using NTier.DataAccess.Entities;
using NTier.DataAccess.Functions.Interfaces;

namespace NTier.DataAccess.Functions.Specific;

public class ApplicationOperations : IApplicationOperations
{
    public async Task<Application> AddFullApplication(
        long gradeId,
        long applicationStatusId,
        int schoolYear,
        string firstName, 
        string surname,
        DateTime birthDate,
        string email,
        string contactNumber)
    {
        try
        {
            using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
            {
                using (var transaction = await context.Database.BeginTransactionAsync())
                {
                    try
                    {
                        var applicant = new Applicant
                        {
                            ApplicantName = firstName,
                            ApplicantSurname = surname,
                            ApplicantBirthDate = birthDate,
                            ContactEmail = email,
                            ContactNumber = contactNumber
                        };

                        var trackingApplicant = await context.Applicants.AddAsync(applicant);

                        await context.SaveChangesAsync();

                        var application = new Application
                        {
                            ApplicantId = applicant.ApplicantId,
                            ApplicationStatusId = applicationStatusId,
                            GradeId = gradeId,
                            SchoolYear = schoolYear
                        };

                        var trackingApplication = await context.Applications.AddAsync(application);

                        await context.SaveChangesAsync();

                        await transaction.CommitAsync();

                        application.Applicant = applicant;

                        return application;
                    }
                    catch
                    {
                        await transaction.RollbackAsync();

                        throw;
                    }
                }
            }
        }
        catch
        {
            throw;
        }
    }

    public async Task<List<Application>> GetApplicationsByApplicantId(long applicantId)
    {
        try
        {
            using (var context = new DatabaseContext(DatabaseContext.Options.DatabaseOptions))
            {
                List<Application> Applications = await context.Applications.Include(a => a.Applicant)
                                                                           .OrderBy(a => a.SchoolYear)
                                                                           .Where(a => a.ApplicantId == applicantId)
                                                                           .ToListAsync();

                return Applications;
            }
        }
        catch
        {
            throw;
        }
    }
}
