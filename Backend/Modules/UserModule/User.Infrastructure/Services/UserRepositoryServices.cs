
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using User.Application.Common.Result;
using User.Application.Contracts;
using User.Core.Constants;
using User.Core.Entity;
using User.Infrastructure.Database;
using User.Infrastructure.Repositories.IRepositories;

namespace User.Infrastructure.Services;

public sealed class UserAccountService(IUserRepository repository, UserDbContext context) : IUserAccountService
{
    // Queries

    public Task<UserModel?> GetByIdAsync(Guid id) =>
        repository.GetByIdAsync(id);

    public Task<UserModel?> GetByEmailAsync(string email) =>
        repository.GetByEmailAsync(email);

    public Task<UserModel?> GetByUserNameAsync(string userName) =>
        repository.GetByUserNameAsync(userName);

    // Roles

    public async Task<IReadOnlyList<string>> GetRolesAsync(UserModel user) =>
        [.. (await repository.GetRolesAsync(user))];

    public Task<bool> IsInRoleAsync(UserModel user, string role) =>
        repository.IsInRoleAsync(user, role);

    public async Task<UserServiceResult> AddToRoleAsync(UserModel user, string role) =>
        Convert(await repository.AddToRoleAsync(user, role));

    public async Task<UserServiceResult> RemoveFromRoleAsync(UserModel user, string role) =>
        Convert(await repository.RemoveFromRoleAsync(user, role));

    // Claims

    public async Task<IReadOnlyList<Claim>> GetClaimsAsync(UserModel user) =>
        [.. (await repository.GetClaimsAsync(user))];

    public async Task<UserServiceResult> AddClaimsAsync(UserModel user, IEnumerable<Claim> claims) =>
        Convert(await repository.AddClaimsAsync(user, claims));
    public async Task<UserServiceResult> RemoveClaimsAsync(UserModel user, IEnumerable<Claim> claims) =>
        Convert(await repository.RemoveClaimsAsync(user, claims));

    // Security

    public Task<string> GetSecurityStampAsync(UserModel user) =>
        repository.GetSecurityStampAsync(user);
    public async Task<UserServiceResult> UpdateSecurityStampAsync(UserModel user) =>
        Convert(await repository.UpdateSecurityStampAsync(user));

    // CRUD

    public async Task<UserServiceResult> UpdateAsync(UserModel user) =>
        Convert(await repository.UpdateAsync(user));
    public async Task<UserServiceResult> DeleteAsync(UserModel user) =>
        Convert(await repository.DeleteAsync(user));

    // Registration

    public Task<UserServiceResult> CreateAsync(UserModel user, string password) =>
        CreateAsync(user, password, null);

    // Additional claims must come only from trusted server-side code.
    public async Task<UserServiceResult> CreateAsync(UserModel user, string password, IEnumerable<Claim>? claims)
    {
        await using var transaction = await context.Database.BeginTransactionAsync();

        var result = await repository.CreateAsync(user, password);

        if (!result.Succeeded)
            return Convert(result);

        result = await repository.AddToRoleAsync(user, UserRoles.User);

        if (!result.Succeeded)
            return Convert(result);

        var userClaims = new List<Claim>
        {
            new(UserClaims.Subscription, SubscriptionTypes.Default)
        };

        if (claims is not null)
        {
            userClaims.AddRange(
                claims.Where(x => x.Type != UserClaims.Subscription));
        }

        result = await repository.AddClaimsAsync(user, userClaims);

        if (!result.Succeeded)
            return Convert(result);

        await transaction.CommitAsync();
        return Convert(IdentityResult.Success);
    }

    // Authentication

    public async Task<UserLoginCheck> LoginAsync(
        string email, string password)
    {
        var user = await repository.GetByEmailAsync(email);

        if (user is null)
            return new UserLoginCheck(null, false, false, false, false);

        var result = await repository.CheckPasswordSignInAsync(
            user, password);

        return new UserLoginCheck(
            result.Succeeded ? user : null,
            result.Succeeded,
            result.IsLockedOut,
            result.IsNotAllowed,
            result.RequiresTwoFactor);
    }

    public Task<bool> IsEmailConfirmedAsync(UserModel user) =>
        repository.IsEmailConfirmedAsync(user);

    // Identity result mapping

    private static UserServiceResult Convert(IdentityResult result) =>
        new(result.Succeeded, [.. result.Errors.Select(x => new UserError(x.Code, x.Description))]);
}
