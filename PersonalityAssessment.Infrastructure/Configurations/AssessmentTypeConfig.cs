using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class AssessmentTypeConfig : IEntityTypeConfiguration<AssessmentType>
    {


        public void Configure(EntityTypeBuilder<AssessmentType> builder)
        {
            builder.HasKey(assessmenttype => assessmenttype.Id);


            builder.Property(assessmenttype => assessmenttype.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(assessmenttype => assessmenttype.IsDeleted).HasDefaultValue(false);

            builder.Property(assessmenttype => assessmenttype.CreatedAt).HasDefaultValueSql("NOW()");
            //builder.ToTable("AssessmentTypes");


        }
    }
}
