using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class PersonalityTypeConfig : IEntityTypeConfiguration<PersonalityType>
    {
        public void Configure(EntityTypeBuilder<PersonalityType> builder)
        {

            builder.HasKey(personalityType => personalityType.Id);

            builder.Property(personalityType => personalityType.Name)
                .IsRequired()
                .HasMaxLength(100);


            builder.Property(personalityType => personalityType.Label)
              .HasMaxLength(1);

            builder.Property(personalityType => personalityType.ImageUrl)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(personalityType => personalityType.Description)
               .IsRequired()
               .HasMaxLength(1000);


            builder.HasIndex(personalityType => personalityType.Name).IsUnique();

            builder.HasIndex(personalityType => personalityType.Label).IsUnique();

            //  builder.ToTable("PersonalityTypes");



            builder.Property(personalityType => personalityType.IsDeleted).HasDefaultValue(false);

            builder.Property(personalityType => personalityType.CreatedAt).HasDefaultValueSql("NOW()");
        }
    }
}
