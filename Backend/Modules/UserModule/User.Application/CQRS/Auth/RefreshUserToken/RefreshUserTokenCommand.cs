using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Requests.Auth;
using User.Application.DTO.Responses.Auth;
namespace User.Application.CQRS.Auth.RefreshUserToken;

public sealed record RefreshUserTokenCommand(RefreshTokenRequest Data) : IRequest<UserResult<AuthTokensResponse>>;
