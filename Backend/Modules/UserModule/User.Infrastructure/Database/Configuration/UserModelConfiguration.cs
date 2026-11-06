
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Core.Entity;

namespace User.Infrastructure.Database.Configuration;

public sealed class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
{
    public void Configure(EntityTypeBuilder<UserModel> builder)
    {
        builder.Property(x => x.AvatarUrl)
            .HasMaxLength(2048)
            .IsRequired(false);

        builder.Property(x => x.Level)
            .HasDefaultValue(1)
            .IsRequired();

        builder.Property(x => x.ExperiencePoints)
            .HasDefaultValue(0)
            .IsRequired();

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.LastActiveAt)
            .IsRequired(false);

        builder.HasMany(x => x.Activities)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.RefreshTokens)
            .WithOne(x => x.User)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
