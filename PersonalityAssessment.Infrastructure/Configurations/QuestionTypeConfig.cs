using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class QuestionTypeConfig : IEntityTypeConfiguration<QuestionType>
    {
        public void Configure(EntityTypeBuilder<QuestionType> builder)
        {

            builder.HasKey(questionType => questionType.Id);
            builder.Property(questionType => questionType.Name)
                .IsRequired()
                .HasMaxLength(200);


            //  builder.ToTable("QuestionTypes");
            builder.Property(questionType => questionType.IsDeleted).HasDefaultValue(false);

            builder.Property(questionType => questionType.CreatedAt).HasDefaultValueSql("NOW()");
        }
    }
}
