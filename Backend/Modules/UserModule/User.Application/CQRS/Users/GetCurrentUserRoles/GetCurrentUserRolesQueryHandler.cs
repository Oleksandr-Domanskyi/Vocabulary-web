using MediatR;
using User.Application.DTO.Responses.Users;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Users.GetCurrentUserRoles;

public sealed class GetCurrentUserRolesQueryHandler(IUserAccountService service) : IRequestHandler<GetCurrentUserRolesQuery, UserResult<UserRolesResponse>>
{
    public async Task<UserResult<UserRolesResponse>> Handle(GetCurrentUserRolesQuery request, CancellationToken cancellationToken)
    {
        var user = await service.GetByIdAsync(request.UserId);

        if (user is null)
            return UserResult<UserRolesResponse>.Fail("NotFound", "User was not found.");

        var roles = await service.GetRolesAsync(user);
        return UserResult<UserRolesResponse>.Ok(new UserRolesResponse(roles));
    }
}
