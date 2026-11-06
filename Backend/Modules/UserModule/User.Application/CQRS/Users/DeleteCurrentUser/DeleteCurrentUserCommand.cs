using MediatR;
using User.Application.Common.Result;
namespace User.Application.CQRS.Users.DeleteCurrentUser;

public sealed record DeleteCurrentUserCommand(Guid UserId) : IRequest<UserResult<bool>>;
