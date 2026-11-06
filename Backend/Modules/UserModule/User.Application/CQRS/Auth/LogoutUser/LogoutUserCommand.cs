using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Requests.Auth;
namespace User.Application.CQRS.Auth.LogoutUser;

public sealed record LogoutUserCommand(LogoutUserRequest Data) : IRequest<UserResult<bool>>;
