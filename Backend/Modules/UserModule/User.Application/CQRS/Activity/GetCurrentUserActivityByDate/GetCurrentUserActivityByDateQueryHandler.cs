using MediatR;
using User.Application.DTO.Responses.Activity;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Activity.GetCurrentUserActivityByDate;

public sealed class GetCurrentUserActivityByDateQueryHandler(IUserActivityService service) : IRequestHandler<GetCurrentUserActivityByDateQuery, UserResult<UserActivityResponse>>
{
    public async Task<UserResult<UserActivityResponse>> Handle(GetCurrentUserActivityByDateQuery request, CancellationToken cancellationToken)
    {
        var activity = await service.GetByDateAsync(request.UserId, request.Date);

        var response = activity is null
            ? new UserActivityResponse(request.Date, 0, 0)
            : new UserActivityResponse(activity.Date, activity.ActivityCount, activity.EarnedExperience);

        return UserResult<UserActivityResponse>.Ok(response);
    }
}
