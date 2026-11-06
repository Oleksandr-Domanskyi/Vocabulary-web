using MediatR;
using User.Application.DTO.Responses.Activity;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Activity.GetCurrentUserStatistics;

public sealed class GetCurrentUserStatisticsQueryHandler(IUserAccountService userService, IUserActivityService activityService) : IRequestHandler<GetCurrentUserStatisticsQuery, UserResult<UserStatisticsResponse>>
{
    public async Task<UserResult<UserStatisticsResponse>> Handle(GetCurrentUserStatisticsQuery request, CancellationToken cancellationToken)
    {
        var user = await userService.GetByIdAsync(request.UserId);

        if (user is null)
            return UserResult<UserStatisticsResponse>.Fail("NotFound", "User was not found.");

        var activities = await activityService.GetByUserIdAsync(request.UserId);
        var streak = await activityService.GetCurrentStreakAsync(request.UserId);

        return UserResult<UserStatisticsResponse>.Ok(new UserStatisticsResponse
        (user.Level, user.ExperiencePoints, activities.Sum(x => x.ActivityCount), activities.Sum(x => x.EarnedExperience), streak));
    }
}
