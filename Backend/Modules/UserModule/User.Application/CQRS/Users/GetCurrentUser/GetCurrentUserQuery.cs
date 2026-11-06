using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Responses.Users;
namespace User.Application.CQRS.Users.GetCurrentUser;

public sealed record GetCurrentUserQuery(Guid UserId) : IRequest<UserResult<UserProfileResponse>>;
