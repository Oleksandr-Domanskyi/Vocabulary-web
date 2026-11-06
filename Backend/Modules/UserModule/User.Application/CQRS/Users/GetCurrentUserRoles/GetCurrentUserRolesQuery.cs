using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Responses.Users;
namespace User.Application.CQRS.Users.GetCurrentUserRoles;

public sealed record GetCurrentUserRolesQuery(Guid UserId) : IRequest<UserResult<UserRolesResponse>>;
