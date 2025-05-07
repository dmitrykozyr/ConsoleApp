using Microsoft.EntityFrameworkCore;
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
                .HasKey(z => z.Applicant_ID);

            modelBuilder.Entity<Applicant>()
                .Property(z => z.Applicant_ID)
                .UseIdentityColumn(1, 1)
                .IsRequired()
                .HasColumnName("applicant_id");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.Applicant_Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("applicant_name");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.Applicant_Surname)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("applicant_surname");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.Applicant_BirthDate)
                .IsRequired(true)
                .HasColumnName("applicant_birthdate");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.Contact_Email)
                .IsRequired(false)
                .HasMaxLength(250)
                .HasColumnName("contact_email");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.Contact_Number)
                .IsRequired(true)
                .HasMaxLength(25)
                .HasColumnName("contact_number");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.Applicant_CreationDate)
                .IsRequired(true)
                .HasDefaultValue(DateTime.UtcNow)
                .HasColumnName("applicant_creationdate");
        
            modelBuilder.Entity<Applicant>()
                .Property(z => z.Applicant_ModifiedDate)
                .IsRequired(true)
                .HasDefaultValue(modifiedDate)
                .HasColumnName("applicant_modifieddate");
        
            modelBuilder.Entity<Applicant>()
                .HasMany(z => z.Applications)
                .WithOne(z => z.Applicant)
                .HasForeignKey(z => z.Applicant_ID)
                .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region ApplicationStatus

            modelBuilder.Entity<ApplicationStatus>()
                .ToTable("application_status");

            modelBuilder.Entity<ApplicationStatus>()
                .HasKey(z => z.ApplicationStatus_ID);
        
            modelBuilder.Entity<ApplicationStatus>()
                .Property(z => z.ApplicationStatus_ID)
                .UseIdentityColumn(1, 1)
                .IsRequired()
                .HasColumnName("application_status_id"); 
        
            modelBuilder.Entity<ApplicationStatus>()
                .HasIndex(z => z.ApplicationStatus_Name)
                .IsUnique();

            modelBuilder.Entity<ApplicationStatus>()
                .Property(z => z.ApplicationStatus_Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("application_status_name");    
        
            modelBuilder.Entity<ApplicationStatus>()
                .Property(z => z.ApplicationStatus_CreationDate)
                .IsRequired(true)
                .HasDefaultValue(DateTime.UtcNow)
                .HasColumnName("application_status_creationdate");

            modelBuilder.Entity<ApplicationStatus>()
                .Property(z => z.ApplicationStatus_ModifiedDate)
                .IsRequired(true)
                .HasDefaultValue(modifiedDate)
                .HasColumnName("application_status_modifieddate");
        
            modelBuilder.Entity<ApplicationStatus>()
                .HasMany(z => z.Applications)
                .WithOne(z => z.ApplicationStatus)
                .HasForeignKey(z => z.ApplicationStatus_ID)
                .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region Grade

            modelBuilder.Entity<Grade>()
                .ToTable("grade");

            modelBuilder.Entity<Grade>()
                .HasKey(z => z.Grade_ID);
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.Grade_ID)
                .UseIdentityColumn(1, 1)
                .IsRequired()
                .HasColumnName("grade_id");
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.Grade_Name)
                .IsRequired(true)
                .HasMaxLength(100)
                .HasColumnName("grade_name");
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.Grade_Number)
                .IsRequired(true)
                .HasColumnName("grade_number");
        
            modelBuilder.Entity<Grade>()
                .HasIndex(z => z.Grade_Name)
                .IsUnique();
        
            modelBuilder.Entity<Grade>()
                .HasIndex(z => z.Grade_Number)
                .IsUnique();
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.Grade_Capacity)
                .IsRequired(true)
                .HasColumnName("grade_capacity");
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.Grade_CreationDate)
                .IsRequired(true)
                .HasDefaultValue(DateTime.UtcNow)
                .HasColumnName("grade_creationdate");
        
            modelBuilder.Entity<Grade>()
                .Property(z => z.Grade_ModifiedDate)
                .IsRequired(true)
                .HasDefaultValue(modifiedDate)
                .HasColumnName("grade_modifieddate");

            modelBuilder.Entity<Grade>()
                .HasMany<Application>(z => z.Applications)
                .WithOne(z => z.Grade)
                .HasForeignKey(z => z.Grade_ID)
                .OnDelete(DeleteBehavior.Restrict);

        #endregion

        #region Application

            modelBuilder.Entity<Application>()
                .ToTable("application");
            
            modelBuilder.Entity<Application>()
                .HasKey(z => z.Application_ID);
        
            modelBuilder.Entity<Application>()
                .Property(z => z.Application_ID)
                .UseIdentityColumn(1, 1)
                .IsRequired()
                .HasColumnName("application_id");

            modelBuilder.Entity<Application>()
                .Property(z => z.Applicant_ID)
                .IsRequired(true)
                .HasColumnName("applicant_id");
            
            modelBuilder.Entity<Application>()
                .Property(z => z.Grade_ID)
                .IsRequired(true)
                .HasColumnName("grade_id");
        
            modelBuilder.Entity<Application>()
                .Property(z => z.ApplicationStatus_ID)
                .IsRequired(true)
                .HasColumnName("application_status_id");
        
            modelBuilder.Entity<Application>()
                .Property(z => z.Application_CreationDate)
                .IsRequired(true)
                .HasDefaultValue(DateTime.UtcNow)
                .HasColumnName("application_creationdate");
        
            modelBuilder.Entity<Application>()
                .Property(z => z.Application_ModifiedDate)
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
                .HasForeignKey(z => z.Applicant_ID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Application>()
                .HasOne<Grade>(z => z.Grade)
                .WithMany(z => z.Applications)
                .HasForeignKey(z => z.Grade_ID)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Application>()
                .HasOne<ApplicationStatus>(z => z.ApplicationStatus)
                .WithMany(z => z.Applications)
                .HasForeignKey(z => z.ApplicationStatus_ID)
                .OnDelete(DeleteBehavior.NoAction);

        #endregion
    }
}
