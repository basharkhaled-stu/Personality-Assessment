using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class StrengthConfig : IEntityTypeConfiguration<Strength>
    {
        public void Configure(EntityTypeBuilder<Strength> builder)
        {
            builder.HasKey(strength => strength.Id);

            builder.Property(strength => strength.Text).IsRequired();
            builder.HasIndex(strength => new { strength.PersonalityTypeId, strength.Text })
           .IsUnique()
           .HasDatabaseName("IX_Strength_Unique")
           .HasFilter(null);

            builder.HasOne(strength => strength.PersonalityType)
                .WithMany(strength => strength.strengths)
                .HasForeignKey(strength => strength.PersonalityTypeId);

            //builder.ToTable("Strengths");

            builder.Property(baseEntity => baseEntity.IsDeleted).HasDefaultValue(false);

            builder.Property(strength => strength.CreatedAt).HasDefaultValueSql("NOW()");

        }
    }
}
