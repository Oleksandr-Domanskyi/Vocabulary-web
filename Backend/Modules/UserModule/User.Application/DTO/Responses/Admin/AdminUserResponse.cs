namespace User.Application.DTO.Responses.Admin;

public sealed record AdminUserResponse(
    Guid UserId,
    string UserName,
    string Email,
    bool EmailConfirmed,
    bool LockoutEnabled,
    DateTimeOffset? LockoutEnd);
