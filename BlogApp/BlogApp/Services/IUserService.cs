using BlogApp.Data.Models;
using BlogApp.ViewModels.Roles;
using BlogApp.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.Services
{
	public interface IUserService
	{
		public Task<IdentityResult> UpdateUserAsync(UserEditViewModel model);
		public Task<UserViewModel> GetUserAsync(string id);
		public Task<UserListViewModel> GetAllUsersAsync();
		public Task<IdentityResult> DeleteUserAsync(string id);
		public RegisterViewModel Register();
		public Task<IdentityResult> Register(RegisterViewModel model);
		public Task<IdentityResult> AdminRegister(RegisterViewModel model);
		public Task<SignInResult> Login(LoginViewModel model);
		public Task<UserEditViewModel> EditUser(ClaimsPrincipal claims);
		public Task<UserEditViewModel> EditUser(string id);
		public Task<UserViewModel> GetCurrentUserAsync(ClaimsPrincipal claims);
	}
}
