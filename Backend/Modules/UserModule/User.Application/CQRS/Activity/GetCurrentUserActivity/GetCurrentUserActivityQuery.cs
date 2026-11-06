using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Responses.Activity;
namespace User.Application.CQRS.Activity.GetCurrentUserActivity;

public sealed record GetCurrentUserActivityQuery(Guid UserId) : IRequest<UserResult<UserActivityHistoryResponse>>;
