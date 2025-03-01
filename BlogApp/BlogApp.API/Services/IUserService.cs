using BlogApp.Data.Models;
using BlogApp.API.ViewModels.Roles;
using BlogApp.API.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.API.Services;

public interface IUserService
{
	public Task<IdentityResult> UpdateUserAsync(UserEditViewModel model);
	public Task<UserViewModel> GetUserAsync(string id);
	public Task<UserListViewModel> GetAllUsersAsync();
	public Task<IdentityResult> DeleteUserAsync(string id);
	public Task<IdentityResult> Register(RegisterViewModel model);
	public Task<IdentityResult> AdminRegister(RegisterViewModel model);
	public Task<SignInResult> Login(LoginViewModel model);
	public Task<UserViewModel> GetCurrentUserAsync(ClaimsPrincipal claims);
}
