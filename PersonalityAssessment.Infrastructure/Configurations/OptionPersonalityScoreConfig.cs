using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Core.Entities;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    public class OptionPersonalityScoreConfig : IEntityTypeConfiguration<OptionPersonalityScore>
    {
        public void Configure(EntityTypeBuilder<OptionPersonalityScore> builder)
        {

            builder.HasKey(optionPersonalityScore => optionPersonalityScore.Id);
            builder.Property(optionPersonalityScore => optionPersonalityScore.Score)
                .HasColumnType("decimal(5,2)")
                .IsRequired(false);


            builder.HasOne(optionPersonalityScore => optionPersonalityScore.Option)
               .WithMany(optionPersonalityScore => optionPersonalityScore.OptionPersonalityScores)
              .HasForeignKey(optionPersonalityScore => optionPersonalityScore.OptionId);


            builder.HasOne(optionPersonalityScore => optionPersonalityScore.PersonalityType)
               .WithMany(optionPersonalityScore => optionPersonalityScore.OptionPersonalityScores)
               .HasForeignKey(optionPersonalityScore => optionPersonalityScore.PersonalityTypeId);


            builder.Property(optionPersonalityScore => optionPersonalityScore.IsDeleted).HasDefaultValue(false);

            builder.Property(optionPersonalityScore => optionPersonalityScore.CreatedAt).HasDefaultValueSql("NOW()");

            //   builder.ToTable("OptionPersonalityScores");



        }
    }
}
