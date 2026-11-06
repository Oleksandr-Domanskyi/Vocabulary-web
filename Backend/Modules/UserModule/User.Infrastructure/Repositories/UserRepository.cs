
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using User.Core.Entity;
using User.Infrastructure.Repositories.IRepositories;

namespace User.Infrastructure.Repositories;

public sealed class UserRepository(UserManager<UserModel> userManager, SignInManager<UserModel> signInManager) : IUserRepository
{
    // Queries
    public Task<UserModel?> GetByIdAsync(Guid id) =>
        userManager.FindByIdAsync(id.ToString());

    public Task<UserModel?> GetByEmailAsync(string email) =>
        userManager.FindByEmailAsync(email);

    public Task<UserModel?> GetByUserNameAsync(string userName) =>
        userManager.FindByNameAsync(userName);

    // CRUD
    public Task<IdentityResult> CreateAsync(UserModel user, string password) =>
        userManager.CreateAsync(user, password);

    public Task<IdentityResult> UpdateAsync(UserModel user) =>
        userManager.UpdateAsync(user);

    public Task<IdentityResult> DeleteAsync(UserModel user) =>
        userManager.DeleteAsync(user);

    // Authentication
    public Task<SignInResult> CheckPasswordSignInAsync(UserModel user, string password) =>
        signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

    public Task<bool> IsEmailConfirmedAsync(UserModel user) =>
        userManager.IsEmailConfirmedAsync(user);

    // Roles
    public Task<IList<string>> GetRolesAsync(UserModel user) =>
        userManager.GetRolesAsync(user);

    public Task<bool> IsInRoleAsync(UserModel user, string role) =>
        userManager.IsInRoleAsync(user, role);

    public Task<IdentityResult> AddToRoleAsync(UserModel user, string role) =>
        userManager.AddToRoleAsync(user, role);

    public Task<IdentityResult> RemoveFromRoleAsync(UserModel user, string role) =>
        userManager.RemoveFromRoleAsync(user, role);

    // Claims
    public Task<IList<Claim>> GetClaimsAsync(UserModel user) =>
        userManager.GetClaimsAsync(user);

    public Task<IdentityResult> AddClaimsAsync(UserModel user, IEnumerable<Claim> claims) =>
        userManager.AddClaimsAsync(user, claims);

    public Task<IdentityResult> RemoveClaimsAsync(UserModel user, IEnumerable<Claim> claims) =>
        userManager.RemoveClaimsAsync(user, claims);

    // Security
    public Task<string> GetSecurityStampAsync(UserModel user) =>
        userManager.GetSecurityStampAsync(user);

    public Task<IdentityResult> UpdateSecurityStampAsync(UserModel user) =>
        userManager.UpdateSecurityStampAsync(user);
}
