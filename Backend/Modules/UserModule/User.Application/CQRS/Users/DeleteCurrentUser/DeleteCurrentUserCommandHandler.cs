using MediatR;
using User.Application.Common.Result;
using User.Application.Contracts;

namespace User.Application.CQRS.Users.DeleteCurrentUser;

public sealed class DeleteCurrentUserCommandHandler(IUserAccountService service) : IRequestHandler<DeleteCurrentUserCommand, UserResult<bool>>
{
    public async Task<UserResult<bool>> Handle(DeleteCurrentUserCommand request, CancellationToken cancellationToken)
    {
        var user = await service.GetByIdAsync(request.UserId);
        if (user is null)
            return UserResult<bool>.Fail("NotFound", "User was not found.");
        var result = await service.DeleteAsync(user);
        return result.Succeeded
            ? UserResult<bool>.Ok(true)
            : UserResult<bool>.Fail(result.Errors);
    }
}
