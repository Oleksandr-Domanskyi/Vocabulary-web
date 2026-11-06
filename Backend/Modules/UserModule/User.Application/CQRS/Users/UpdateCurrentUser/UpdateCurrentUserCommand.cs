using MediatR;
using User.Application.Common.Result;
using User.Application.DTO.Requests.Users;
using User.Application.DTO.Responses.Users;

namespace User.Application.CQRS.Users.UpdateCurrentUser;

public sealed record UpdateCurrentUserCommand(Guid UserId, UpdateUserProfileRequest Data) : IRequest<UserResult<UserProfileResponse>>;
