using MediatR;
using User.Application.DTO.Responses.Users;
using User.Core.Constants;
using User.Application.Contracts;
using User.Application.Common.Result;

namespace User.Application.CQRS.Users.GetCurrentUserClaims;

public sealed class GetCurrentUserClaimsQueryHandler(IUserAccountService service) : IRequestHandler<GetCurrentUserClaimsQuery, UserResult<UserClaimsResponse>>
{
    public async Task<UserResult<UserClaimsResponse>> Handle(GetCurrentUserClaimsQuery request, CancellationToken cancellationToken)
    {
        var user = await service.GetByIdAsync(request.UserId);
        if (user is null)
            return UserResult<UserClaimsResponse>.Fail("NotFound", "User was not found.");

        var claims = await service.GetClaimsAsync(user);

        var visible = claims
            .Where(x => x.Type == UserClaims.Subscription)
            .Select(x => new UserClaimResponse(x.Type, x.Value))
            .ToArray();

        return UserResult<UserClaimsResponse>.Ok(new UserClaimsResponse(visible));
    }
}
