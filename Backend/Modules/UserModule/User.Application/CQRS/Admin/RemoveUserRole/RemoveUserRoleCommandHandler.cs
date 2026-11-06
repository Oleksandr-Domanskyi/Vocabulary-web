using MediatR;
using User.Application.Common.Result;
using User.Application.Contracts;

namespace User.Application.CQRS.Admin.RemoveUserRole;

public sealed class RemoveUserRoleCommandHandler(IUserAccountService service) : IRequestHandler<RemoveUserRoleCommand, UserResult<bool>>
{
    public async Task<UserResult<bool>> Handle(RemoveUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await service.GetByIdAsync(request.UserId);
        if (user is null)
            return UserResult<bool>.Fail("NotFound", "User was not found.");

        var result = await service.RemoveFromRoleAsync(user, request.Role);

        return result.Succeeded
            ? UserResult<bool>.Ok(true)
            : UserResult<bool>.Fail(result.Errors);
    }
}
