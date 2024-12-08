using BlogApp.ViewModels;
using BlogApp.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Reflection.Metadata;

namespace BlogApp.Controllers
{
	public class RoleController : Controller
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		public RoleController(RoleManager<IdentityRole> roleManager)
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
		[Authorize("Admin")]
		[Route("CreateRole")]
		[HttpGet]
		public IActionResult CreateRole()
		{
			return Ok(new CreateRoleViewModel());
		}
		[Authorize("Admin")]
		[Route("CreateRole")]
		[HttpPost]
		public async Task<IActionResult> CreateRole(CreateRoleViewModel role)
		{
			var newRole = new IdentityRole()
			{
				Name = role.Name,
				NormalizedName = role.Name.ToUpper()
			};
			if (await _roleManager.GetRoleNameAsync(newRole) == null)
			{
				await _roleManager.CreateAsync(newRole);
			}
			return Ok();
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
		public async Task CreateBaseRoles()
		{
			var admin = new IdentityRole()
			{
				Name = "Admin",
				NormalizedName = "ADMIN"
			};
			var user = new IdentityRole()
			{
				Name = "User",
				NormalizedName = "USER"
			};
			var moderator = new IdentityRole()
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

