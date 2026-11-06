using User.Core.Entity;

namespace User.Application.Contracts;

public interface IUserActivityService
{
    Task<UserActivity?> GetByDateAsync(Guid userId, DateOnly date);
    Task<List<UserActivity>> GetByUserIdAsync(Guid userId);
    Task<UserActivity> RecordActivityAsync(Guid userId, int earnedExperience);
    Task<int> GetCurrentStreakAsync(Guid userId);
}
