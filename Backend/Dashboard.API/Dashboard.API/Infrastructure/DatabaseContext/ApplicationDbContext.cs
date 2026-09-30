using Dashboard.API.Domain.Entities;
using Dashboard.API.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Dashboard.API.Infrastructure.DatabaseContext
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<JobApplication> JobApplications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<JobApplication>(entity =>
            {
                // Primary Key
                entity.HasKey(e => e.Id);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                // Fields Configuration
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(100);
                entity.Property(e => e.JobTitle).IsRequired().HasMaxLength(100);
                entity.Property(e => e.CreatedAt).IsRequired();
                entity.Property(e => e.Status).IsRequired();

                // 1. Get all integer values from the Enum
                var enumValues = Enum.GetValues(typeof(StatusEnum))
                                     .Cast<int>();

                // 2. Create the SQL string: "0, 1, 2"
                var sqlValues = string.Join(", ", enumValues);

                // 3. Add the Check Constraint
                // SQL: CHECK ([Status] IN (0, 1, 2)
                entity.ToTable(t =>
                    t.HasCheckConstraint("CK_JobApplication_Status",
                    $"([Status] IN ({sqlValues}) )")
                );

                entity.ToTable(t => t.HasCheckConstraint("CK_JobApplication_Name_MinLength", "LEN([Name]) >= 2"));

                entity.ToTable(t => t.HasCheckConstraint("CK_JobApplication_Email_MinLength", "LEN([Email]) >= 5"));

                entity.ToTable(t => t.HasCheckConstraint("CK_JobApplication_JobTitle_MinLength", "LEN([JobTitle]) >= 2"));

                //here we are checking the length of fields as we want them
            });
        }
    }
}