using MediatR;
using User.Core.Constants;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Admin.AddUserRole;

public sealed class AddUserRoleCommandHandler(IUserAccountService service) : IRequestHandler<AddUserRoleCommand, UserResult<bool>>
{
    public async Task<UserResult<bool>> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
    {
        if (request.Data.Role != UserRoles.User && request.Data.Role != UserRoles.Admin)
            return UserResult<bool>.Fail("InvalidRole", "The requested role is not allowed.");

        var user = await service.GetByIdAsync(request.UserId);
        if (user is null)
            return UserResult<bool>.Fail("NotFound", "User was not found.");

        var result = await service.AddToRoleAsync(user, request.Data.Role);

        return result.Succeeded
            ? UserResult<bool>.Ok(true)
            : UserResult<bool>.Fail(result.Errors);
    }
}
