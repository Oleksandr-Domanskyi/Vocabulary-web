namespace User.Application.DTO.Responses.Users;

public sealed record UserClaimsResponse(IReadOnlyList<UserClaimResponse> Claims);
