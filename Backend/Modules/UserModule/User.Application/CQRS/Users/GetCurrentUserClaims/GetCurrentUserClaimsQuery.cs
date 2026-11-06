using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Responses.Users;
namespace User.Application.CQRS.Users.GetCurrentUserClaims;

public sealed record GetCurrentUserClaimsQuery(Guid UserId) : IRequest<UserResult<UserClaimsResponse>>;
