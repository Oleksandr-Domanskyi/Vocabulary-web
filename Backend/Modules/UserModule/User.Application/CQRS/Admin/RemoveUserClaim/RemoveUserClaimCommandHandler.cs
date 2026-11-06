using MediatR;
using System.Security.Claims;
using User.Core.Constants;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Admin.RemoveUserClaim;

public sealed class RemoveUserClaimCommandHandler(IUserAccountService service) : IRequestHandler<RemoveUserClaimCommand, UserResult<bool>>
{
    public async Task<UserResult<bool>> Handle(RemoveUserClaimCommand request, CancellationToken cancellationToken)
    {
        if (request.Data.Type != UserClaims.Subscription)
            return UserResult<bool>.Fail("InvalidClaim", "This claim type cannot be removed through this operation.");

        var user = await service.GetByIdAsync(request.UserId);
        if (user is null)
            return UserResult<bool>.Fail("NotFound", "User was not found.");

        var result = await service.RemoveClaimsAsync(user,
            [new Claim(request.Data.Type, request.Data.Value)]);

        return result.Succeeded
            ? UserResult<bool>.Ok(true)
            : UserResult<bool>.Fail(result.Errors);
    }
}
