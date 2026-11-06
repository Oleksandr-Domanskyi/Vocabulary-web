using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Requests.Auth;
using User.Application.DTO.Responses.Auth;
namespace User.Application.CQRS.Auth.LoginUser;

public sealed record LoginUserCommand(LoginUserRequest Data) : IRequest<UserResult<AuthTokensResponse>>;
