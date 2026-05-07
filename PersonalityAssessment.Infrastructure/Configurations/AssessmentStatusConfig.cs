using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class AssessmentStatusConfig : IEntityTypeConfiguration<AssessmentStatus>
    {
        public void Configure(EntityTypeBuilder<AssessmentStatus> builder)
        {
            builder.HasKey(assessmentstatus => assessmentstatus.Id);
            builder.Property(assessmentstatus => assessmentstatus.Name)
                .IsRequired()
                .HasMaxLength(200);


            builder.Property(assessmentstatus => assessmentstatus.IsDeleted).HasDefaultValue(false);

            builder.Property(assessmentstatus => assessmentstatus.CreatedAt).HasDefaultValueSql("NOW()");
            //builder.ToTable("AssessmentStatuses");
        }
    }
}
