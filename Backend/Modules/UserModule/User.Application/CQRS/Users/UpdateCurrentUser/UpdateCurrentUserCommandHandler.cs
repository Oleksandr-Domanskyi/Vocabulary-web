using MediatR;
using User.Application.DTO.Responses.Users;
using User.Application.Contracts;
using User.Application.Common.Result;
using User.Application.Mappers;
namespace User.Application.CQRS.Users.UpdateCurrentUser;

public sealed class UpdateCurrentUserCommandHandler(IUserAccountService service) : IRequestHandler<UpdateCurrentUserCommand, UserResult<UserProfileResponse>>
{
    public async Task<UserResult<UserProfileResponse>> Handle(UpdateCurrentUserCommand request, CancellationToken cancellationToken)
    {
        var user = await service.GetByIdAsync(request.UserId);

        if (user is null)
            return UserResult<UserProfileResponse>.Fail("NotFound", "User was not found.");

        user.UserName = request.Data.UserName;
        user.AvatarUrl = request.Data.AvatarUrl;

        var result = await service.UpdateAsync(user);

        return result.Succeeded
            ? UserResult<UserProfileResponse>.Ok(UserResponseMapper.ToProfile(user))
            : UserResult<UserProfileResponse>.Fail(result.Errors);
    }
}
