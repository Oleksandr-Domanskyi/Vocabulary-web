using System.Security.Claims;
using User.Application.Common.Result;
using User.Core.Entity;

namespace User.Application.Contracts;

public interface IUserAccountService
{
    Task<UserModel?> GetByIdAsync(Guid id);
    Task<UserModel?> GetByEmailAsync(string email);
    Task<UserModel?> GetByUserNameAsync(string userName);
    Task<UserServiceResult> CreateAsync(UserModel user, string password);
    Task<UserServiceResult> UpdateAsync(UserModel user);
    Task<UserServiceResult> DeleteAsync(UserModel user);
    Task<UserLoginCheck> LoginAsync(string email, string password);
    Task<bool> IsEmailConfirmedAsync(UserModel user);
    Task<IReadOnlyList<string>> GetRolesAsync(UserModel user);
    Task<bool> IsInRoleAsync(UserModel user, string role);
    Task<UserServiceResult> AddToRoleAsync(UserModel user, string role);
    Task<UserServiceResult> RemoveFromRoleAsync(UserModel user, string role);
    Task<IReadOnlyList<Claim>> GetClaimsAsync(UserModel user);
    Task<UserServiceResult> AddClaimsAsync(UserModel user, IEnumerable<Claim> claims);
    Task<UserServiceResult> RemoveClaimsAsync(UserModel user, IEnumerable<Claim> claims);
    Task<string> GetSecurityStampAsync(UserModel user);
    Task<UserServiceResult> UpdateSecurityStampAsync(UserModel user);
}

public sealed record UserServiceResult(bool Succeeded, IReadOnlyList<UserError> Errors);
public sealed record UserLoginCheck(UserModel? User, bool Succeeded, bool IsLockedOut, bool IsNotAllowed, bool RequiresTwoFactor);
