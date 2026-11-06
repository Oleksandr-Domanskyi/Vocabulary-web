using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Responses.Activity;
namespace User.Application.CQRS.Activity.GetCurrentUserStatistics;

public sealed record GetCurrentUserStatisticsQuery(Guid UserId) : IRequest<UserResult<UserStatisticsResponse>>;
