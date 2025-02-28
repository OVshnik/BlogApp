using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Exceptions;
using BlogApp.ViewModels.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BlogApp.Services
{
	public class RoleService : IRoleService
	{
		private readonly RoleManager<Role> _roleManager;
		private readonly UserManager<User> _userManager;
		private readonly IMapper _mapper;
		public RoleService(RoleManager<Role> roleManager, IMapper mapper, UserManager<User> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
			_mapper = mapper;
		}
		/// <summary>
		/// Метод для создания роли
		/// </summary>
		public async Task<IdentityResult> CreateRoleAsync(CreateRoleViewModel model)
		{
			var role = _mapper.Map<Role>(model);

			return await _roleManager.CreateAsync(role);
		}
		/// <summary>
		/// Метод для редактирования роли
		/// </summary>
		public async Task<EditRoleViewModel> EditRoleAsync(string id)
		{
			var role = await _roleManager.FindByIdAsync(id);

			var model = _mapper.Map<EditRoleViewModel>(role);

			return model;
		}
		/// <summary>
		/// Метод для обновления роли
		/// </summary>
		public async Task<IdentityResult> UpdateRoleAsync(EditRoleViewModel model)
		{
			var role = await _roleManager.FindByIdAsync(model.Id);
			if (role != null)
			{
				role.Name = model.Name;
				role.Description = model.Description;
				var result = await _roleManager.UpdateAsync(role);
				return result;
			}
			return IdentityResult.Failed();
		}
		/// <summary>
		/// Метод для получения роли
		/// </summary>
		public async Task<RoleViewModel> GetRoleAsync(string id)
		{
			var findRole = await _roleManager.FindByIdAsync(id);
			if (findRole != null)
			{
				var role = _mapper.Map<RoleViewModel>(findRole);
				return role;
			}
			throw new ModelNotFoundException($"Роль с id={id} не удалось получить из БД");
		}
		/// <summary>
		/// Метод для получения всех ролей
		/// </summary>
		public async Task<ListRolesViewModel> GetAllRolesAsync()
		{
			var allRoles = await _roleManager.Roles.ToListAsync();
			var roles = new ListRolesViewModel();
			if (allRoles != null)
			{
				foreach (var role in allRoles)
				{
					roles.Roles.Add(_mapper.Map<RoleViewModel>(role));
				}
				return roles;
			}
			return roles;
		}
		/// <summary>
		/// Метод для удаления роли
		/// </summary>
		public async Task DeleteRoleAsync(string id, ClaimsPrincipal claims)
		{
			var user = await _userManager.GetUserAsync(claims);
			var role = await _roleManager.FindByIdAsync(id);

			if (role != null && user != null && role.Name != null)
			{
				if (!await _userManager.IsInRoleAsync(user, role.Name))
				{
					await _roleManager.DeleteAsync(role);
				}
			}
		}
		/// <summary>
		/// Метод для проверки уникальности имени роли
		/// </summary>
		public async Task<Role> CheckNameAsync(string name)
		{
			var role = await _roleManager.FindByNameAsync(name);
			if (role != null) 
			return role;
			throw new ModelNotFoundException($"Роль с именем {name} не удалось получить из БД");
		}
	}
}
