using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using User.Core.Entity;

namespace User.Infrastructure.Repositories.IRepositories;

public interface IUserRepository
{
    // Queries
    Task<UserModel?> GetByIdAsync(Guid id);
    Task<UserModel?> GetByEmailAsync(string email);
    Task<UserModel?> GetByUserNameAsync(string userName);

    // CRUD
    Task<IdentityResult> CreateAsync(UserModel user, string password);
    Task<IdentityResult> UpdateAsync(UserModel user);
    Task<IdentityResult> DeleteAsync(UserModel user);

    // Authentication
    Task<SignInResult> CheckPasswordSignInAsync(UserModel user, string password);
    Task<bool> IsEmailConfirmedAsync(UserModel user);

    // Roles
    Task<IList<string>> GetRolesAsync(UserModel user);
    Task<bool> IsInRoleAsync(UserModel user, string role);
    Task<IdentityResult> AddToRoleAsync(UserModel user, string role);
    Task<IdentityResult> RemoveFromRoleAsync(UserModel user, string role);

    // Claims
    Task<IList<Claim>> GetClaimsAsync(UserModel user);
    Task<IdentityResult> AddClaimsAsync(UserModel user, IEnumerable<Claim> claims);
    Task<IdentityResult> RemoveClaimsAsync(UserModel user, IEnumerable<Claim> claims);

    // Security
    Task<string> GetSecurityStampAsync(UserModel user);
    Task<IdentityResult> UpdateSecurityStampAsync(UserModel user);
}
