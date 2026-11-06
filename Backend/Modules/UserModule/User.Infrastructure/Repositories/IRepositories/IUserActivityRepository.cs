using User.Core.Entity;

namespace User.Infrastructure.Repositories.IRepositories;

public interface IUserActivityRepository
{
    Task<UserActivity?> GetByDateAsync(Guid userId, DateOnly date);
    Task<List<UserActivity>> GetByUserIdAsync(Guid userId);
    Task<UserActivity> CreateAsync(UserActivity activity);
    Task<UserActivity> UpdateAsync(UserActivity activity);
}
