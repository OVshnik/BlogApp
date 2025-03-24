using BlogApp.Models;
using BlogApp.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BlogApp.Controllers;

public class HomeController : Controller
{
	private readonly ILogger<HomeController> _logger;
	private readonly IRoleService _roleService;
	private readonly IUserService _userService;

	public HomeController(ILogger<HomeController> logger, IRoleService roleService, IUserService userService)
	{
		_logger = logger;
		_roleService = roleService;
		_userService = userService;	
	}

	public async Task<IActionResult> Index()
	{
		//Добавление базовых ролей//
		await _roleService.CreateBaseRoles();
		//Добавление тестового суперпользователя, который может создать пользователя с правами администратора//
		await _userService.AddUberAdmin();
		return View();
	}

	public IActionResult Privacy()
	{
		return View();
	}

	[NonAction]
	[Route("Error")]
	public IActionResult Error(int? statusCode = null)
	{
		if (statusCode.HasValue)
		{
			if (statusCode == 404 || statusCode == 403|| statusCode==500)
			{
				var viewName = statusCode.ToString();
				_logger.LogWarning($"Произошла ошибка - {statusCode}\n{viewName}");
				return View(viewName);
			}
			else
				return View("500");
		}
		return View("500");
	}
}
