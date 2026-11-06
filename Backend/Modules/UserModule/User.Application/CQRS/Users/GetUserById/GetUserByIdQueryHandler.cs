using MediatR;
using User.Application.DTO.Responses.Users;
using User.Application.Contracts;
using User.Application.Common.Result;
using User.Application.Mappers;

namespace User.Application.CQRS.Users.GetUserById;

public sealed class GetUserByIdQueryHandler(IUserAccountService service) : IRequestHandler<GetUserByIdQuery, UserResult<PublicUserProfileResponse>>
{
    public async Task<UserResult<PublicUserProfileResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var user = await service.GetByIdAsync(request.UserId);
        return user is null
            ? UserResult<PublicUserProfileResponse>.Fail("NotFound", "User was not found.")
            : UserResult<PublicUserProfileResponse>.Ok(UserResponseMapper.ToPublicProfile(user));
    }
}
