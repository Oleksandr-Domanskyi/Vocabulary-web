using Backend.Shared.Database;
using Microsoft.EntityFrameworkCore;
using User.Core.Entity;

namespace User.Infrastructure.Database;

public class UserDbContext(DbContextOptions<UserDbContext> options) : GlobalVocabularyDbContext<UserModel>(options)
{
    protected override string Schema => "User";

    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    public DbSet<UserActivity> UserActivities { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new Configuration.UserModelConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.RefreshTokenConfiguration());
        modelBuilder.ApplyConfiguration(new Configuration.UserActivityConfiguration());
    }

}
