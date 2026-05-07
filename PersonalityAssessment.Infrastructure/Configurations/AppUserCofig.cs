using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonalityAssessment.Infrastructure.User;

namespace PersonalityAssessment.Infrastructure.Configurations
{
    internal class AppUserCofig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(appUser => appUser.FirstName)
                 .IsRequired()
                .HasMaxLength(100);


            builder.Property(appUser => appUser.LastName)
                 .IsRequired()
                .HasMaxLength(100);

            builder.Property(appUser => appUser.ProfileImageUrl)
                .HasMaxLength(512);

            builder.Property(appUser => appUser.GoogleId)
                .HasMaxLength(128);

            builder.HasIndex(appUser => appUser.GoogleId)
                .IsUnique();

            /*   builder.Property(appUser => appUser.FullName)
                  .HasComputedColumnSql("FirstName || ' ' || LastName", stored: true)
                     .HasMaxLength(201);*/


            //  builder.HasIndex(appUser => appUser.FullName);
        }
    }
}
