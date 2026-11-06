using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Shared.Database;

public abstract class GlobalVocabularyDbContext(DbContextOptions options) : DbContext(options)
{
    public abstract string Schema { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schema);
    }
}

public abstract class GlobalVocabularyDbContext<TUser>(DbContextOptions options) : IdentityDbContext<TUser, IdentityRole<Guid>, Guid>(options)
    where TUser : IdentityUser<Guid>
{
    protected abstract string Schema { get; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.HasDefaultSchema(Schema);
    }
}
