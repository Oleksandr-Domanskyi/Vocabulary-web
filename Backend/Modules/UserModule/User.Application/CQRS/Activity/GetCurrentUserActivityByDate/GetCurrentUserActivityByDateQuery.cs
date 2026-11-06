using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Responses.Activity;
namespace User.Application.CQRS.Activity.GetCurrentUserActivityByDate;

public sealed record GetCurrentUserActivityByDateQuery(Guid UserId, DateOnly Date) : IRequest<UserResult<UserActivityResponse>>;
