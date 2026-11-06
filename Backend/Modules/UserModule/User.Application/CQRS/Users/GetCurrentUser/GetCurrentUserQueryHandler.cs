using MediatR;
using User.Application.DTO.Responses.Users;
using User.Application.Contracts;
using User.Application.Common.Result;
using User.Application.Mappers;
namespace User.Application.CQRS.Users.GetCurrentUser;

public sealed class GetCurrentUserQueryHandler(IUserAccountService service) : IRequestHandler<GetCurrentUserQuery, UserResult<UserProfileResponse>>
{
    public async Task<UserResult<UserProfileResponse>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
    {
        var user = await service.GetByIdAsync(request.UserId);
        return user is null
            ? UserResult<UserProfileResponse>.Fail("NotFound", "User was not found.")
            : UserResult<UserProfileResponse>.Ok(UserResponseMapper.ToProfile(user));
    }
}
