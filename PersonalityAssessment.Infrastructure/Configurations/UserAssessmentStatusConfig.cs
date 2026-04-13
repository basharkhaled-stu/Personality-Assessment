using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class UserAssessmentStatusConfig : IEntityTypeConfiguration<UserAssessmentStatus>
    {
        public void Configure(EntityTypeBuilder<UserAssessmentStatus> builder)
        {
            builder.HasKey(userAssessmentStatus => userAssessmentStatus.Id);
            builder.Property(userAssessmentStatus => userAssessmentStatus.Name)
                .IsRequired()
                .HasColumnType("text");


            //  builder.ToTable("UserAssessmentStatuses");

            builder.Property(userAssessmentStatus => userAssessmentStatus.IsDeleted).HasDefaultValue(false);

            builder.Property(userAssessmentStatus => userAssessmentStatus.CreatedAt).HasDefaultValueSql("NOW()");
        }
    }
}
