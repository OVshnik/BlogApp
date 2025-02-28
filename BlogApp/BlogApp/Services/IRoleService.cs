using BlogApp.Data.Models;
using BlogApp.ViewModels.Roles;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.Services
{
	public interface IRoleService
	{
		public Task<IdentityResult> CreateRoleAsync(CreateRoleViewModel model);
		public Task<IdentityResult> UpdateRoleAsync(EditRoleViewModel model);
		public Task<RoleViewModel> GetRoleAsync(string id);
		public Task<ListRolesViewModel> GetAllRolesAsync();
		public Task DeleteRoleAsync(string id, ClaimsPrincipal claims);
		public Task<EditRoleViewModel> EditRoleAsync(string id);
		public Task<Role> CheckNameAsync(string name);
	}
}
