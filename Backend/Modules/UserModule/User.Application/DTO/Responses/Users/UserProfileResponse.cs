namespace User.Application.DTO.Responses.Users;

public sealed record UserProfileResponse(
    Guid UserId,
    string UserName,
    string Email,
    bool EmailConfirmed,
    string? AvatarUrl,
    int Level,
    int ExperiencePoints,
    DateTime CreatedAt,
    DateTime? LastActiveAt);
