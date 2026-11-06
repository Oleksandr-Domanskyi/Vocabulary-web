using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Responses.Users;

namespace User.Application.CQRS.Users.GetUserById;

public sealed record GetUserByIdQuery(Guid UserId) : IRequest<UserResult<PublicUserProfileResponse>>;
