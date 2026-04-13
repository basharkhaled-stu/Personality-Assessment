using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class AssessmentConfig : IEntityTypeConfiguration<Assessment>
    {
        public void Configure(EntityTypeBuilder<Assessment> builder)
        {
            builder.HasKey(assessment => assessment.Id);
            builder.Property(assessment => assessment.Title)
                .IsRequired()
                 .HasColumnType("text");


            builder.Property(assessment => assessment.Description)
             .IsRequired();

            builder.HasOne(assessment => assessment.AssessmentStatus)
                .WithMany(assessment => assessment.Assessments)
                .HasForeignKey(assessment => assessment.AssessmentStatusId);


            builder.HasOne(assessment => assessment.AssessmentType)
               .WithMany(assessment => assessment.Assessments)
               .HasForeignKey(assessment => assessment.AssessmentTypeId);



            builder.Property(assessment => assessment.IsDeleted).HasDefaultValue(false);

            builder.Property(assessment => assessment.CreatedAt).HasDefaultValueSql("NOW()");
            // builder.ToTable("Assessments");

        }
    }
}
