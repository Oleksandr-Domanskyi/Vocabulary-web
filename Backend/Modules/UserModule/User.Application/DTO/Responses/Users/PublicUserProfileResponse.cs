namespace User.Application.DTO.Responses.Users;

public sealed record PublicUserProfileResponse(
    Guid UserId,
    string UserName,
    string? AvatarUrl,
    int Level);
