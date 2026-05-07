using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PersonalityAssessment.Core.Entities;
using PersonalityAssessment.Infrastructure.User;
using System.Linq.Expressions;

namespace PersonalityAssessment.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {


        public DbSet<Assessment> Assessments { get; set; }
        public DbSet<AssessmentStatus> AssessmentStatuses { get; set; }
        public DbSet<AssessmentType> AssessmentTypes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<QuestionType> QuestionTypes { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<OptionPersonalityScore> OptionPersonalityScores { get; set; }
        public DbSet<PersonalityType> PersonalityTypes { get; set; }
        public DbSet<Strength> Strengths { get; set; }
        public DbSet<Weakness> Weaknesses { get; set; }
        public DbSet<UsersAssessment> UsersAssessments { get; set; }
        public DbSet<UserAssessmentStatus> UserAssessmentStatuses { get; set; }
        public DbSet<UsersAssessmentResult> UsersAssessmentResults { get; set; }
        public DbSet<UsersAssessmentResultPersonalityType> UsersAssessmentResultPersonalityTypes { get; set; }
        public DbSet<UserAnswer> UserAnswers { get; set; }
        public DbSet<AppUser> appUsers { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.ClrType.GetProperty("IsDeleted") != null)
                {
                    var parameter = Expression.Parameter(entityType.ClrType, "e");
                    var property = Expression.Property(parameter, "IsDeleted");
                    var condition = Expression.Equal(property, Expression.Constant(false));
                    var lambda = Expression.Lambda(condition, parameter);

                    modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                }
            }

        }
    }
}
