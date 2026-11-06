using MediatR;
using User.Application.DTO.Responses.Activity;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Activity.GetCurrentUserActivity;

public sealed class GetCurrentUserActivityQueryHandler(IUserActivityService service) : IRequestHandler<GetCurrentUserActivityQuery, UserResult<UserActivityHistoryResponse>>
{
    public async Task<UserResult<UserActivityHistoryResponse>> Handle(GetCurrentUserActivityQuery request, CancellationToken cancellationToken)
    {
        var activities = await service.GetByUserIdAsync(request.UserId);

        var days = activities.Select(a => new UserActivityResponse(
            a.Date, a.ActivityCount, a.EarnedExperience)).ToArray();

        return UserResult<UserActivityHistoryResponse>.Ok(new UserActivityHistoryResponse(days));
    }
}
