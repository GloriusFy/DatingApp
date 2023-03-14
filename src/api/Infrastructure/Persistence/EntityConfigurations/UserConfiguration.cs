using Domain;
using Domain.Aggregates.Users.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Persistence.EntityConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder
                .Property(u => u.Gender)
                .HasConversion(new EnumToStringConverter<Gender>());

            builder.OwnsOne(u => u.Password);
            builder.OwnsMany(u => u.Photos).ToTable("photo");

            builder.OwnsMany(u => u.UserLikes, onb =>
            {
                onb.ToTable("user_like");

                onb
                    .HasOne(l => l.SourceUser)
                    .WithMany(u => u.UserLikes)
                    .HasForeignKey(l => l.SourceUserId);

                onb
                    .HasOne<User>()
                    .WithMany()
                    .HasForeignKey(l => l.TargetUserId);

                onb.HasKey(l => new {l.SourceUserId, l.TargetUserId});
            });
        }
    }
}
