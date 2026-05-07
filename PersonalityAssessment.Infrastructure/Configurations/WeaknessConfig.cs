using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class WeaknessConfig : IEntityTypeConfiguration<Weakness>
    {
        public void Configure(EntityTypeBuilder<Weakness> builder)
        {
            builder.HasKey(weakness => weakness.Id);

            builder.Property(weakness => weakness.Text).IsRequired();
            builder.HasIndex(weakness => new { weakness.PersonalityTypeId, weakness.Text })
           .IsUnique()
           .HasDatabaseName("IX_weakness_Unique")
           .HasFilter(null);

            builder.HasOne(weakness => weakness.PersonalityType)
                .WithMany(weakness => weakness.Weaknesses)
                .HasForeignKey(weakness => weakness.PersonalityTypeId);


            builder.Property(weakness => weakness.IsDeleted).HasDefaultValue(false);

            builder.Property(weakness => weakness.CreatedAt).HasDefaultValueSql("NOW()");
            //  builder.ToTable("Weaknesses");
        }
    }
}
