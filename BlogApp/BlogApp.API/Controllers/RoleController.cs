using BlogApp.API.Services;
using BlogApp.API.ViewModels.Roles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace BlogApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RoleController : ControllerBase
	{
		private readonly IRoleService _roleService;
		public RoleController(IRoleService roleService)
		{
			_roleService = roleService;
		}
		[Authorize("OnlyAdmin")]
		[HttpPost]
		[Route("AddRole")]
		public async Task<IActionResult> AddRole(CreateRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _roleService.CreateRoleAsync(model);
				if (result.Succeeded)
					return StatusCode(201);
				else
				{
					return StatusCode(501);
				}
			}
			else
			{
				return BadRequest("Указанные данные не прошли валидацию");
			}
		}
		[Authorize("OnlyAdmin")]
		[HttpGet]
		[Route("GetRole")]
		public async Task<IActionResult> GetRole(string id)
		{
			var role = await _roleService.GetRoleAsync(id);
			if (role != null)
			{
				return StatusCode(200, role);
			}
			return NotFound();
		}
		[Authorize("OnlyAdmin")]
		[HttpGet]
		[Route("GetAllRoles")]
		public async Task<IActionResult> GetAllRoles()
		{
			var roles = await _roleService.GetAllRolesAsync();
			if (roles != null)
			{
				return StatusCode(200, roles);
			}
			return NotFound();
		}
		[Authorize("OnlyAdmin")]
		[HttpPatch]
		[Route("EditRole")]
		public async Task<IActionResult> EditRole(EditRoleViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _roleService.UpdateRoleAsync(model);
				if (result.Succeeded)
					return StatusCode(200);
				else
					return StatusCode(501);
			}
			else
			{
				return BadRequest("Указанные данные не прошли валидацию");
			}
		}
		[Authorize("OnlyAdmin")]
		[HttpDelete]
		[Route("DeleteRole")]
		public async Task<IActionResult> DeleteRole(string id)
		{
			await _roleService.DeleteRoleAsync(id, User);
			return StatusCode(200);
		}
	}
}
