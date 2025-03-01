using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace BlogApp.Controllers;

    public class AuthenticateController : Controller
{
	private readonly IMapper _mapper;
	private readonly SignInManager<User> _signInManager;
	private readonly IUserService _userService;
	private readonly ILogger<AuthenticateController> _logger;
	public AuthenticateController(SignInManager<User> signInManager, IMapper mapper,IUserService userService, ILogger<AuthenticateController> logger)
	{
		_signInManager = signInManager;
		_mapper = mapper;
		_userService = userService;
		_logger = logger;
	}
	/// <summary>
	/// [Get] Метод, вход пользователя в систему
	/// </summary>
	[Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
	/// <summary>
	/// [Post] Метод, выход пользователя
	/// </summary>
	[Route("Logout")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Logout()
	{
		if(User.Identity!=null)
		_logger.LogInformation($"Пользователь с логином {User.Identity.Name} вышел из системы");

		await _signInManager.SignOutAsync();
		return RedirectToAction("Index","Home");
	}
	/// <summary>
	/// [Get] Метод, вход пользователя в систему
	/// </summary>
	[HttpGet]
	public IActionResult Login(string returnUrl="")
	{
		return View(new LoginViewModel { ReturnUrl = returnUrl });
	}
	/// <summary>
	/// [Post] Метод, вход пользователя в систему
	/// </summary>
	[Route("Login")]
	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginViewModel model)
	{
		if (ModelState.IsValid)
		{
			var result = await _userService.Login(model);

			if (result.Succeeded)
			{
				_logger.LogInformation($"Пользователь с логином {model.Email} вошел в систему");
				return RedirectToAction("Index","Home");
			}
			else
			{
				ModelState.AddModelError("", "Неправильный логин и (или) пароль");
			}
		}
		return View("Login");
	}
}
