using MediatR;
using User.Application.DTO.Responses.Activity;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Activity.GetCurrentUserStreak;

public sealed class GetCurrentUserStreakQueryHandler(IUserActivityService service) : IRequestHandler<GetCurrentUserStreakQuery, UserResult<UserStreakResponse>>
{
    public async Task<UserResult<UserStreakResponse>> Handle(GetCurrentUserStreakQuery request, CancellationToken cancellationToken)
    {
        var streak = await service.GetCurrentStreakAsync(request.UserId);
        return UserResult<UserStreakResponse>.Ok(new UserStreakResponse(streak));
    }
}
