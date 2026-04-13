using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class UsersAssessmentConfig : IEntityTypeConfiguration<UsersAssessment>
    {
        public void Configure(EntityTypeBuilder<UsersAssessment> builder)
        {

            builder.HasKey(usersAssessment => usersAssessment.Id);
            builder.Property(usersAssessment => usersAssessment.UserId)
                .IsRequired()
                .HasMaxLength(450);

            builder.Property(usersAssessment => usersAssessment.StartedAt)
                   .HasDefaultValueSql("NOW()")
                      .IsRequired();


            builder.Property(usersAssessment => usersAssessment.CompletedAt)
                   .HasDefaultValue(null)
                      .IsRequired(false);


            builder.HasOne(usersAssessment => usersAssessment.UserAssessmentStatus)
                .WithMany(usersAssessment => usersAssessment.UsersAssessments)
                .HasForeignKey(usersAssessment => usersAssessment.UserAssessmentStatusId);

            builder.HasIndex(usersAssessment => usersAssessment.UserId);

            //  builder.ToTable("UsersAssessments");
            builder.Property(usersAssessment => usersAssessment.IsDeleted).HasDefaultValue(false);

            builder.Property(usersAssessment => usersAssessment.CreatedAt).HasDefaultValueSql("NOW()");
        }
    }
}
