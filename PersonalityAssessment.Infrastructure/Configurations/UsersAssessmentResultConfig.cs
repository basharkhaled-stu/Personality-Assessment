using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class UsersAssessmentResultConfig : IEntityTypeConfiguration<UsersAssessmentResult>
    {
        public void Configure(EntityTypeBuilder<UsersAssessmentResult> builder)
        {

            builder.HasKey(usersAssessmentResult => usersAssessmentResult.Id);
            builder.HasOne(usersAssessmentResult => usersAssessmentResult.usersAssessment)
                .WithMany(usersAssessmentResult => usersAssessmentResult.UsersAssessmentResults)
                .HasForeignKey(usersAssessmentResult => usersAssessmentResult.UsersAssessmentId);



            builder.Property(usersAssessmentResult => usersAssessmentResult.IsDeleted).HasDefaultValue(false);

            builder.Property(usersAssessmentResult => usersAssessmentResult.CreatedAt).HasDefaultValueSql("NOW()");
            // builder.ToTable("UsersAssessmentResults");
        }
    }
}
