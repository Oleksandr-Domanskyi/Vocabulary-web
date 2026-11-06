using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Requests.Auth;
using User.Application.DTO.Responses.Auth;
namespace User.Application.CQRS.Auth.RegisterUser;

public sealed record RegisterUserCommand(RegisterUserRequest Data) : IRequest<UserResult<RegisterUserResponse>>;
