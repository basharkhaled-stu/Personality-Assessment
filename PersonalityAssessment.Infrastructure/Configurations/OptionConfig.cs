using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class OptionConfig : IEntityTypeConfiguration<Option>
    {
        public void Configure(EntityTypeBuilder<Option> builder)
        {

            builder.HasKey(option => option.Id);
            builder.Property(option => option.Text)
                .IsRequired()
                .HasMaxLength(1000);


            builder.HasIndex(option => new { option.QuestionId, option.DisplayOrder })
                .IsUnique();

            builder.HasOne(option => option.Question)
                .WithMany(option => option.Options)
                .HasForeignKey(option => option.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(option => option.IsDeleted).HasDefaultValue(false);

            builder.Property(option => option.CreatedAt).HasDefaultValueSql("NOW()");
            //builder.ToTable("Options");
        }
    }
}
