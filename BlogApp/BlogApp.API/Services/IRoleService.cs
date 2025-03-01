using BlogApp.Data.Models;
using BlogApp.API.ViewModels.Roles;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.API.Services;

public interface IRoleService
{
	public Task<IdentityResult> CreateRoleAsync(CreateRoleViewModel model);
	public Task<IdentityResult> UpdateRoleAsync(EditRoleViewModel model);
	public Task<RoleViewModel> GetRoleAsync(string id);
	public Task<ListRolesViewModel> GetAllRolesAsync();
	public Task DeleteRoleAsync(string id, ClaimsPrincipal claims);
	public Task<Role> CheckNameAsync(string name);
}
