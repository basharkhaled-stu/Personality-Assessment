using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class QuestionConfig : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.HasKey(question => question.Id);

            builder.Property(question => question.Text)
                .IsRequired()
                  .HasMaxLength(1000);

            builder.HasIndex(question => new { question.AssessmentId, question.DisplayOrder })
                .IsUnique();


            builder.HasOne(question => question.Assessment)
                .WithMany(question => question.Questions)
                .HasForeignKey(question => question.AssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(question => question.QuestionType)
                .WithMany(question => question.Questions)
                .HasForeignKey(question => question.QuestionTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.ToTable("Questions");
            builder.Property(question => question.IsDeleted).HasDefaultValue(false);

            builder.Property(question => question.CreatedAt).HasDefaultValueSql("NOW()");

        }
    }
}
