using BlogApp.ViewModels;
using BlogApp.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Reflection.Metadata;
using BlogApp.Data.Models;
using BlogApp.Services;
namespace BlogApp.Controllers
{
	public class RoleController : Controller
	{
		private readonly RoleManager<Role> _roleManager;
		public RoleController(RoleManager<Role> roleManager)
		{
			_roleManager = roleManager;
		}
		[Authorize("Admin")]
		[Route("CreateBaseRoles")]
		[HttpGet]
		public async Task<IActionResult> CreateBaseRole()
		{
			await CreateBaseRoles();
			return Ok();
		}
		//[Authorize("Admin")]
		[Route("AddRole")]
		[HttpGet]
		public IActionResult CreateRole()
		{
			return View("AddRole", new CreateRoleViewModel());
		}
		//[Authorize("Admin")]
		[Route("AddRole")]
		[HttpPost]
		public async Task<IActionResult> CreateRole(CreateRoleViewModel role)
		{
			var newRole = new Role()
			{
				Name = role.Name,
				NormalizedName = role.Name.ToUpper(),
				Description = role.Description,
			};
			await _roleManager.CreateAsync(newRole);
			return RedirectToPage("/RolesList");
		}
		//[Authorize("Admin")]
		[Route("RolesList")]
		[HttpGet]
		public IActionResult GetAllRoles()
		{
			var allRoles = _roleManager.Roles.ToList();
			if (allRoles != null)
			{
				var roles = new ListRolesViewModel()
				{
					Roles = allRoles
				};
				return View("RolesList", roles);
			}
			return RedirectToPage("/AddRole");
		}
		[Authorize("Admin")]
		[Route("GetRoleById")]
		[HttpGet]
		public async Task<IActionResult> GetRoleById(string id)
		{
			var findRole = await _roleManager.FindByIdAsync(id);
			if (findRole != null)
			{
				var role = new RoleViewModel(findRole);
				return Ok(role);
			}
			return Ok();
		}
		[Authorize("Admin")]
		[Route("GetRoleByName")]
		[HttpGet]
		public async Task<IActionResult> GetRoleByName(string name)
		{
			var findRole = await _roleManager.FindByNameAsync(name);
			if (findRole != null)
			{
				var role = new RoleViewModel(findRole);
				return Ok(role);
			}
			return Ok();
		}
		[Authorize("Admin")]
		[Route("EditRole")]
		[HttpGet]
		public async Task<IActionResult> EditRole(string id)
		{
			var findRole = await _roleManager.FindByIdAsync(id);
			if (findRole != null)
			{
				var role = new EditRoleViewModel()
				{
					Id = findRole.Id,
					Name = findRole.Name,
					NormalizedName = findRole.NormalizedName,
					Description = findRole.Description,
				};
				return Ok(role);

			}
			return Ok();
		}
		[Authorize("Admin")]
		[Route("UpdateRole")]
		[HttpPost]
		public async Task<IActionResult> UpdateRole(EditRoleViewModel model)
		{
			var findRole = await _roleManager.FindByIdAsync(model.Id);
			if (findRole != null)
			{
				findRole.Name = model.Name;
				findRole.NormalizedName = model.NormalizedName;
				await _roleManager.UpdateAsync(findRole);
			}
			return Ok();
		}
		[AcceptVerbs("Get", "Post")]
		public async Task<IActionResult> CheckRoleName(string name)
		{
			var tags = await _roleManager.FindByNameAsync(name);
			if (tags == null)
			{
				return Json(true);
			}
			return Json(false);
		}
		public async Task CreateBaseRoles()
		{
			var admin = new Role()
			{
				Name = "Admin",
				NormalizedName = "ADMIN"
			};
			var user = new Role()
			{
				Name = "User",
				NormalizedName = "USER"
			};
			var moderator = new Role()
			{
				Name = "Moderator",
				NormalizedName = "MODERATOR"
			};
			await _roleManager.CreateAsync(admin);
			await _roleManager.CreateAsync(user);
			await _roleManager.CreateAsync(moderator);
		}

	}
}

