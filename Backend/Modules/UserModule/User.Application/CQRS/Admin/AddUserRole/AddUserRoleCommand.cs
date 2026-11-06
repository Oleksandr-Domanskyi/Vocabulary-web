using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Requests.Admin;
namespace User.Application.CQRS.Admin.AddUserRole;

public sealed record AddUserRoleCommand(Guid UserId, AddUserRoleRequest Data) : IRequest<UserResult<bool>>;
