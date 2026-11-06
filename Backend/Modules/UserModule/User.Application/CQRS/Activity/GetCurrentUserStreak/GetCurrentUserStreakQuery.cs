using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Responses.Activity;
namespace User.Application.CQRS.Activity.GetCurrentUserStreak;

public sealed record GetCurrentUserStreakQuery(Guid UserId) : IRequest<UserResult<UserStreakResponse>>;
