using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class UserAnswerConfig : IEntityTypeConfiguration<UserAnswer>
    {
        public void Configure(EntityTypeBuilder<UserAnswer> builder)
        {

            builder.HasKey(userAnswer => userAnswer.Id);

            builder.HasOne(userAnswer => userAnswer.UsersAssessment)
                .WithMany(userAnswer => userAnswer.UserAnswers)
                .HasForeignKey(userAnswer => userAnswer.UsersAssessmentId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(userAnswer => userAnswer.Question)
                .WithMany(userAnswer => userAnswer.UserAnswers)
                .HasForeignKey(userAnswer => userAnswer.QuestionId)
                 .OnDelete(DeleteBehavior.Restrict);


            builder.HasOne(userAnswer => userAnswer.Option)
                .WithMany(userAnswer => userAnswer.UserAnswers)
                .HasForeignKey(userAnswer => userAnswer.OptionId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.ToTable("UserAnswers");
            builder.Property(userAnswer => userAnswer.IsDeleted).HasDefaultValue(false);

            builder.Property(userAnswer => userAnswer.CreatedAt).HasDefaultValueSql("NOW()");
        }
    }
}
