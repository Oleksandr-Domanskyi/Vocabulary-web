
using Microsoft.EntityFrameworkCore;
using User.Core.Entity;
using User.Infrastructure.Database;
using User.Infrastructure.Repositories.IRepositories;

namespace User.Infrastructure.Repositories;

public class UserActivityRepository(UserDbContext context) : IUserActivityRepository
{
    public Task<UserActivity?> GetByDateAsync(Guid userId, DateOnly date)
    {
        return context.UserActivities.FirstOrDefaultAsync(x => x.UserId == userId && x.Date == date);
    }

    public Task<List<UserActivity>> GetByUserIdAsync(Guid userId)
    {
        return context.UserActivities
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.Date)
            .ToListAsync();
    }

    public async Task<UserActivity> CreateAsync(UserActivity activity)
    {
        context.UserActivities.Add(activity);
        await context.SaveChangesAsync();

        return activity;
    }

    public async Task<UserActivity> UpdateAsync(UserActivity activity)
    {
        context.UserActivities.Update(activity);
        await context.SaveChangesAsync();

        return activity;
    }
}
