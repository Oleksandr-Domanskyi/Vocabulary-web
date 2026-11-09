using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using User.Application.Contracts;
using User.Application.DTO.Responses.Auth;
using User.Core.Entity;
using User.Infrastructure.Options;

namespace User.Infrastructure.Services;

public sealed class AuthTokenService(IRefreshTokenService refreshTokenService, IUserAccountService userAccountService, IOptions<JwtOptions> options) : IAuthTokenService
{
    private readonly JwtOptions _options = options.Value;

    // Token generation

    public async Task<AuthTokensResponse> IssueAsync(UserModel user, IReadOnlyList<string> roles, IReadOnlyList<Claim> claims, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var now = DateTimeOffset.UtcNow;
        var accessExpiresAt = now.AddMinutes(_options.AccessTokenMinutes);

        var tokenClaims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        tokenClaims.AddRange(roles.Select(role => new Claim("role", role)));

        var reservedClaims = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
                "sub", "jti", "iss", "aud", "exp", "nbf", "iat",
                "role", ClaimTypes.NameIdentifier, ClaimTypes.Role
        };

        tokenClaims.AddRange(claims.Where(x => !reservedClaims.Contains(x.Type)));

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: tokenClaims,
            notBefore: now.UtcDateTime,
            expires: accessExpiresAt.UtcDateTime,
            signingCredentials: credentials);

        var accessToken = new JwtSecurityTokenHandler().WriteToken(jwt);

        var refreshToken = Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

        cancellationToken.ThrowIfCancellationRequested();

        await refreshTokenService.CreateAsync(new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            TokenHash = HashToken(refreshToken),
            CreatedAt = now.UtcDateTime,
            ExpiresAt = now.AddDays(_options.RefreshTokenDays).UtcDateTime
        });

        return new AuthTokensResponse(accessToken, refreshToken, accessExpiresAt);
    }

    // Token rotation

    public async Task<AuthTokensResponse?> RefreshAsync(string rawRefreshToken, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(rawRefreshToken))
            return null;

        var tokenHash = HashToken(rawRefreshToken);

        var storedToken = await refreshTokenService.GetValidTokenAsync(tokenHash);

        if (storedToken is null)
            return null;

        var user = await userAccountService.GetByIdAsync(storedToken.UserId);

        if (user is null)
            return null;

        var roles = await userAccountService.GetRolesAsync(user);
        var claims = await userAccountService.GetClaimsAsync(user);

        cancellationToken.ThrowIfCancellationRequested();

        var revoked = await refreshTokenService.RevokeAsync(tokenHash);

        if (!revoked)
            return null;

        return await IssueAsync(user, roles, claims, cancellationToken);
    }

    // Logout

    public async Task<bool> LogoutAsync(string rawRefreshToken, CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();

        if (string.IsNullOrWhiteSpace(rawRefreshToken))
            return false;

        return await refreshTokenService.RevokeAsync(HashToken(rawRefreshToken));
    }

    // Hashing

    private static string HashToken(string token) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));
}
