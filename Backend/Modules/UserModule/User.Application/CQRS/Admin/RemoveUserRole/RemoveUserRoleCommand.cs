using MediatR;
using User.Application.Common.Result;
namespace User.Application.CQRS.Admin.RemoveUserRole;

public sealed record RemoveUserRoleCommand(Guid UserId, string Role) : IRequest<UserResult<bool>>;
