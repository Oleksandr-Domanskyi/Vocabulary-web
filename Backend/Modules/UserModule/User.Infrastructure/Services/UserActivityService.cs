using User.Application.Contracts;
using User.Core.Entity;
using User.Infrastructure.Repositories.IRepositories;

namespace User.Infrastructure.Services;

public sealed class UserActivityService(IUserActivityRepository repository) : IUserActivityService
{
    // Queries

    public Task<UserActivity?> GetByDateAsync(Guid userId, DateOnly date) => repository.GetByDateAsync(userId, date);
    public Task<List<UserActivity>> GetByUserIdAsync(Guid userId) => repository.GetByUserIdAsync(userId);

    // Activity

    public async Task<UserActivity> RecordActivityAsync(Guid userId, int earnedExperience)
    {
        ArgumentOutOfRangeException.ThrowIfNegative(earnedExperience);

        var today = DateOnly.FromDateTime(DateTime.UtcNow);

        var activity = await repository.GetByDateAsync(userId, today);

        if (activity is null)
        {
            return await repository.CreateAsync(new UserActivity
            {
                UserId = userId,
                Date = today,
                ActivityCount = 1,
                EarnedExperience = earnedExperience
            });
        }

        activity.ActivityCount++;
        activity.EarnedExperience += earnedExperience;

        return await repository.UpdateAsync(activity);
    }

    // Streak

    public async Task<int> GetCurrentStreakAsync(Guid userId)
    {
        var activities = await repository.GetByUserIdAsync(userId);

        if (activities.Count == 0)
            return 0;

        var today = DateOnly.FromDateTime(DateTime.UtcNow);
        var expectedDate = today;

        // Preserve the streak if the user has not been active today.
        if (activities[0].Date != today)
            expectedDate = today.AddDays(-1);

        var streak = 0;

        foreach (var activity in activities)
        {
            if (activity.Date != expectedDate)
                break;

            streak++;
            expectedDate = expectedDate.AddDays(-1);
        }

        return streak;
    }
}
