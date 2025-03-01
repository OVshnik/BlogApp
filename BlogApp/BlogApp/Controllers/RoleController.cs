using BlogApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Reflection.Metadata;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.Users;
using BlogApp.ViewModels.Roles;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Data;
namespace BlogApp.Controllers;

public class RoleController : Controller
{
	private readonly RoleManager<Role> _roleManager;
	private readonly IRoleService _roleService;
	private readonly ILogger<RoleController> _logger;
	public RoleController(RoleManager<Role> roleManager, IRoleService roleService, ILogger<RoleController> logger)
	{
		_roleManager = roleManager;
		_roleService = roleService;
		_logger = logger;
	}
	/// <summary>
	/// [Get] Метод, создание новой роли пользователя (только для администратора)
	/// </summary>
	[Authorize("OnlyAdmin")]
	[Route("AddRole")]
	[HttpGet]
	public IActionResult CreateRole()
	{
		return View("AddRole", new CreateRoleViewModel());
	}
	/// <summary>
	/// [Post] Метод, создание новой роли пользователя (только для администратора)
	/// </summary>
	[Authorize("OnlyAdmin")]
	[Route("AddRole")]
	[HttpPost]
	public async Task<IActionResult> CreateRole(CreateRoleViewModel role)
	{
		if (ModelState.IsValid)
		{
			var result = await _roleService.CreateRoleAsync(role);
			if (result.Succeeded)
			{
				_logger.LogInformation($"Создана роль с именем {role.Name}");
				return RedirectToAction("GetAllRoles");
			}
			return RedirectToAction("CreateRole");
		}
		else
		{
			ModelState.AddModelError("", "Некорректные значения");
			string errorMessages = "";
			foreach (var item in ModelState)
			{
				if (item.Value.ValidationState == ModelValidationState.Invalid)
				{
					errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
					foreach (var error in item.Value.Errors)
					{
						errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
					}
				}
			}
			return RedirectToAction("CreateRole");
		}
	}
	/// <summary>
	/// [Get] Метод, список пользователей
	/// </summary>
	[Authorize("OnlyAdmin")]
	[Route("RolesList")]
	[HttpGet]
	public async Task<IActionResult> GetAllRoles()
	{
		var roles = await _roleService.GetAllRolesAsync();
		if (roles.Roles.Count != 0)
		{
			return View("RolesList", roles);
		}
		return RedirectToPage("/AddRole");
	}
	/// <summary>
	/// [Get] Метод, страница пользователя
	/// </summary>
	[Authorize("OnlyAdmin")]
	[Route("GetRole")]
	[HttpGet]
	public async Task<IActionResult> GetRole(string id)
	{
		var role = await _roleService.GetRoleAsync(id);
		return View("RolePage", role);
	}
	/// <summary>
	/// [Get] Метод, редактирование пользователя
	/// </summary>
	[Authorize("OnlyAdmin")]
	[Route("EditRole")]
	[HttpPost]
	public async Task<IActionResult> EditRole(string id)
	{
		var model = await _roleService.EditRoleAsync(id);
		return View(model);
	}
	/// <summary>
	/// [Post] Метод, обновление данных пользователя
	/// </summary>
	[Authorize("OnlyAdmin")]
	[Route("UpdateRole")]
	[HttpPost]
	public async Task<IActionResult> UpdateRole(EditRoleViewModel model)
	{
		if (ModelState.IsValid)
		{
			var result = await _roleService.UpdateRoleAsync(model);
			if (result.Succeeded)
			{
				_logger.LogDebug($"Изменены данные роли с именем {model.Name}");
				return RedirectToAction("GetAllRoles");
			}
			return RedirectToAction("EditRole");
		}
		else
		{
			ModelState.AddModelError("", "Некорректные значения");
			string errorMessages = "";
			foreach (var item in ModelState)
			{
				if (item.Value.ValidationState == ModelValidationState.Invalid)
				{
					errorMessages = $"{errorMessages}\nОшибки для свойства {item.Key}:\n";
					foreach (var error in item.Value.Errors)
					{
						errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
					}
				}
			}
			return RedirectToAction("EditRole");
		}
	}
	/// <summary>
	/// [Post] Метод, удаление пользователя
	/// </summary>
	[Authorize("OnlyAdmin")]
	[Route("DeleteRole")]
	[HttpPost]
	public async Task<IActionResult> DeleteRole(string id)
	{
		await _roleService.DeleteRoleAsync(id, User);
		_logger.LogDebug($"Удалена роль с id={id}");
		return RedirectToAction("GetAllRoles");

	}
	/// <summary>
	/// Метод, проверки имени роли пользователя на уникальность
	/// </summary>
	[AcceptVerbs("Get", "Post")]
	public async Task<IActionResult> CheckRoleName(string name)
	{
		var role = await _roleService.CheckNameAsync(name);
		if (role == null)
		{
			return Json(true);
		}
		return Json(false);
	}

}

