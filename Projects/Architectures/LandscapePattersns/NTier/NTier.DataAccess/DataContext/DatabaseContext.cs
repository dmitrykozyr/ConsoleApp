﻿using Microsoft.EntityFrameworkCore;
using NTier.DataAccess.Entities;

namespace NTier.DataAccess.DataContext;

public class DatabaseContext : DbContext
{
    public class OptionsBuild
    {
        public OptionsBuild()
        {
            Settings = new AppConfiguration();

            OptionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            OptionsBuilder.UseSqlServer(Settings.SqlConnectionString);

            DatabaseOptions = OptionsBuilder.Options;
        }

        public DbContextOptions<DatabaseContext> DatabaseOptions { get; set; }

        public DbContextOptionsBuilder<DatabaseContext> OptionsBuilder { get; set; }

        private AppConfiguration Settings { get; set; }
    }

    public static OptionsBuild Options = new OptionsBuild();

    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options) { }

    public DbSet<Applicant> Applicants { get; set; }

    public DbSet<Application> Applications { get; set; }

    public DbSet<ApplicationStatus> ApplicationStatuses { get; set; }

    public DbSet<Grade> Grades { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        DateTime modifiedDate = new DateTime(1990, 1, 1);

        #region Applicant

            modelBuilder.Entity<Applicant>()
                .ToTable("applicant");
        
            modelBuilder.Entity<Applicant>()
                .HasKey(z => z.ApplicantId);

            modelBuilder.Entity<Applicant>()
                .Property(z => z.ApplicantId)
                .UseIdentityColumn(1, 1)
                .IsRequired()
                .HasColumnName("applicant_id");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.ApplicantName)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("applicant_name");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.ApplicantSurname)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("applicant_surname");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.ApplicantBirthDate)
                .IsRequired(true)
                .HasColumnName("applicant_birthdate");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.ContactEmail)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnName("contact_email");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.ContactNumber)
                .IsRequired(true)
                .HasMaxLength(25)
                .HasColumnName("contact_number");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.ApplicantCreationDate)
                .IsRequired(true)
                .HasDefaultValue(DateTime.UtcNow)
                .HasColumnName("applicant_creationdate");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.ApplicantModifiedDate)
                .IsRequired(true)
                .HasDefaultValue(modifiedDate)
                .HasColumnName("applicant_modifieddate");
        
            modelBuilder.Entity<Applicant>()
                .HasMany(z => z.Applications)
                .WithOne(z => z.Applicant)
                .HasForeignKey(z => z.ApplicantId)
                .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region ApplicationStatus

            modelBuilder.Entity<ApplicationStatus>()
                .ToTable("application_status");

            modelBuilder.Entity<ApplicationStatus>()
                .HasKey(z => z.ApplicationStatusId);
        
            modelBuilder.Entity<ApplicationStatus>()
                .Property(z => z.ApplicationStatusId)
                .UseIdentityColumn(1, 1)
                .IsRequired()
                .HasColumnName("application_status_id"); 
        
            modelBuilder.Entity<ApplicationStatus>()
                .HasIndex(z => z.ApplicationStatusName)
                .IsUnique();

            modelBuilder.Entity<ApplicationStatus>()
                .Property(z => z.ApplicationStatusName)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("application_status_name");    
        
            modelBuilder.Entity<ApplicationStatus>()
                .Property(z => z.ApplicationStatusCreationDate)
                .IsRequired(true)
                .HasDefaultValue(DateTime.UtcNow)
                .HasColumnName("application_status_creationdate");

            modelBuilder.Entity<ApplicationStatus>()
                .Property(z => z.ApplicationStatusModifiedDate)
                .IsRequired(true)
                .HasDefaultValue(modifiedDate)
                .HasColumnName("application_status_modifieddate");
        
            modelBuilder.Entity<ApplicationStatus>()
                .HasMany(z => z.Applications)
                .WithOne(z => z.ApplicationStatus)
                .HasForeignKey(z => z.ApplicationStatusId)
                .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region Grade

            modelBuilder.Entity<Grade>()
                .ToTable("grade");

            modelBuilder.Entity<Grade>()
                .HasKey(z => z.GradeId);
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.GradeId)
                .UseIdentityColumn(1, 1)
                .IsRequired()
                .HasColumnName("grade_id");
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.GradeName)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("grade_name");
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.GradeNumber)
                .IsRequired(true)
                .HasColumnName("grade_number");
        
            modelBuilder.Entity<Grade>()
                .HasIndex(z => z.GradeName)
                .IsUnique();
        
            modelBuilder.Entity<Grade>()
                .HasIndex(z => z.GradeNumber)
                .IsUnique();
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.GradeCapacity)
                .IsRequired(true)
                .HasColumnName("grade_capacity");
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.GradeCreationDate)
                .IsRequired(true)
                .HasDefaultValue(DateTime.UtcNow)
                .HasColumnName("grade_creationdate");
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.GradeModifiedDate)
                .IsRequired(true)
                .HasDefaultValue(modifiedDate)
                .HasColumnName("grade_modifieddate");

            modelBuilder.Entity<Grade>()
                .HasMany<Application>(z => z.Applications)
                .WithOne(z => z.Grade)
                .HasForeignKey(z => z.GradeId)
                .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region Application

            modelBuilder.Entity<Application>()
                .ToTable("application");
            
            modelBuilder.Entity<Application>()
                .HasKey(z => z.ApplicationId);
        
            modelBuilder.Entity<Application>()
                .Property(z => z.ApplicationId)
                .UseIdentityColumn(1, 1)
                .IsRequired()
                .HasColumnName("application_id");

            modelBuilder.Entity<Application>()
                .Property(z => z.ApplicantId)
                .IsRequired(true)
                .HasColumnName("applicant_id");
            
            modelBuilder.Entity<Application>()
                .Property(z => z.GradeId)
                .IsRequired(true)
                .HasColumnName("grade_id");
        
            modelBuilder.Entity<Application>()
                .Property(z => z.ApplicationStatusId)
                .IsRequired(true)
                .HasColumnName("application_status_id");
        
            modelBuilder.Entity<Application>()
                .Property(z => z.ApplicationCreationDate)
                .IsRequired(true)
                .HasDefaultValue(DateTime.UtcNow)
                .HasColumnName("application_creationdate");
        
            modelBuilder.Entity<Application>()
                .Property(z => z.ApplicationModifiedDate)
                .IsRequired(true)
                .HasDefaultValue(modifiedDate)
                .HasColumnName("application_modifieddate");
        
            modelBuilder.Entity<Application>()
                .Property(z => z.SchoolYear)
                .IsRequired(true)
                .HasColumnName("application_schoolyear");

            modelBuilder.Entity<Application>()
                .HasOne<Applicant>(z => z.Applicant)
                .WithMany(z => z.Applications)
                .HasForeignKey(z => z.ApplicantId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Application>()
                .HasOne<Grade>(z => z.Grade)
                .WithMany(z => z.Applications)
                .HasForeignKey(z => z.GradeId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Application>()
                .HasOne<ApplicationStatus>(z => z.ApplicationStatus)
                .WithMany(z => z.Applications)
                .HasForeignKey(z => z.ApplicationStatusId)
                .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}
