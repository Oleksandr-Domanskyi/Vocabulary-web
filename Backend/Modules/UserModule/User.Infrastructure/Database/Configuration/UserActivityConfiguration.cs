
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using User.Core.Entity;

namespace User.Infrastructure.Database.Configuration;

public sealed class UserActivityConfiguration : IEntityTypeConfiguration<UserActivity>
{
    public void Configure(EntityTypeBuilder<UserActivity> builder)
    {
        builder.ToTable("UserActivities");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new { x.UserId, x.Date })
            .IsUnique();

        builder.Property(x => x.Date)
            .IsRequired();

        builder.Property(x => x.ActivityCount)
            .HasDefaultValue(0);

        builder.Property(x => x.EarnedExperience)
            .HasDefaultValue(0);

        builder.HasOne(x => x.User)
            .WithMany(x => x.Activities)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
