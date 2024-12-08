using BlogApp.Data.Models;
using BlogApp.Data.UOW;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace BlogApp.Services
{
	public class UserService
	{
		private readonly UserManager<User> _userManager;
		public UserService(UserManager<User> userManager)
		{
			_userManager = userManager;
		}
		public async Task<IdentityResult> AddNewUserAsync(User newUser, string password)
		{
			var result = await _userManager.CreateAsync(newUser, password);
			if (result.Succeeded)
				await _userManager.AddToRoleAsync(newUser, "User");
			return result;
		}
		public async Task<User> GetUserByIdAsync(string id)
		{
			return  await _userManager.FindByIdAsync(id);
		}
		public async Task<User> GetCurrentUserAsync(ClaimsPrincipal? claims)
		{
			return await _userManager.GetUserAsync(claims);
		}
		public async Task<List<User>> GetAllUsersAsync()
		{
			var userList = _userManager.Users.AsEnumerable().OrderBy(x => x.LastName).ToList();

			return await Task.Run(() => userList);
		}
		public async Task<IdentityResult> UpdateUserAsync(User user)
		{
			return await _userManager.UpdateAsync(user);
		}
		public async Task DeleteUserAsync(string id)
		{
			var delUser = await _userManager.FindByIdAsync(id);
			await _userManager.DeleteAsync(delUser);
		}

	}
}
