using Microsoft.AspNetCore.Identity;

namespace User.Core.Entity;

public class UserModel : IdentityUser<Guid>
{
    public string? AvatarUrl { get; set; }

    public int Level { get; set; } = 1;
    public int ExperiencePoints { get; set; } = 0;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastActiveAt { get; set; }

    public List<UserActivity> Activities { get; set; } = [];

    public List<RefreshToken> RefreshTokens { get; set; } = [];
}
