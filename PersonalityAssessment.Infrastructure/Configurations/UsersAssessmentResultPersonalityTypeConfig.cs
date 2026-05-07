using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class UsersAssessmentResultPersonalityTypeConfig : IEntityTypeConfiguration<UsersAssessmentResultPersonalityType>
    {
        public void Configure(EntityTypeBuilder<UsersAssessmentResultPersonalityType> builder)
        {

            builder.HasKey(o => o.Id);
            builder.Property(o => o.Score).HasColumnType("decimal(5,2)");
            builder.Property(o => o.Rank).HasColumnType("decimal(5,2)");


            builder.HasOne(o => o.PersonalityType)
                .WithMany(o => o.UsersAssessmentResultPersonalityType)
                .HasForeignKey(o => o.PersonalityTypeId);


            builder.HasOne(o => o.UsersAssessmentResult)
                .WithMany(o => o.UsersAssessmentResultPersonalityType)
                .HasForeignKey(o => o.UsersAssessmentResultId);

            builder.Property(o => o.IsDeleted).HasDefaultValue(false);

            builder.Property(o => o.CreatedAt).HasDefaultValueSql("NOW()");
            //builder.ToTable("UsersAssessmentResultPersonalityTypes");
        }
    }
}
