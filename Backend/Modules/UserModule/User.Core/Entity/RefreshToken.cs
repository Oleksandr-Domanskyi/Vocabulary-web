namespace User.Core.Entity;

public class RefreshToken
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }
    public UserModel User { get; set; } = default!;

    public string TokenHash { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime ExpiresAt { get; set; }
    public DateTime? RevokedAt { get; set; }
}
