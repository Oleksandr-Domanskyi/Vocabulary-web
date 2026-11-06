using MediatR;
using static User.Core.Constants.SubscriptionTypes;
using System.Security.Claims;
using User.Core.Constants;
using User.Application.Contracts;
using User.Application.Common.Result;
namespace User.Application.CQRS.Admin.AddUserClaim;

public sealed class AddUserClaimCommandHandler(IUserAccountService service) : IRequestHandler<AddUserClaimCommand, UserResult<bool>>
{
    public async Task<UserResult<bool>> Handle(AddUserClaimCommand request, CancellationToken cancellationToken)
    {

        var user = await service.GetByIdAsync(request.UserId);

        if (user is null)
            return UserResult<bool>.Fail("NotFound", "User was not found.");

        var current = await service.GetClaimsAsync(user);
        if (current.Any(x => x.Type == UserClaims.Subscription))
            return UserResult<bool>.Fail("ClaimAlreadyExists", "Remove or replace the existing subscription claim first.");

        var result = await service.AddClaimsAsync(user, [new Claim(request.Data.Type, request.Data.Value)]);

        return result.Succeeded
            ? UserResult<bool>.Ok(true)
            : UserResult<bool>.Fail(result.Errors);
    }
}
