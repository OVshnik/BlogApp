using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogApp.Controllers
{
    public class RegisterController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly SignInManager<User> _signInManager;
		private readonly ILogger<RegisterController> _logger;
		public RegisterController(IUserService userService, SignInManager<User> signInManager, IMapper mapper, ILogger<RegisterController> logger)
		{
			_userService = userService;
			_signInManager = signInManager;
			_mapper = mapper;
			_logger = logger;

		}
		/// <summary>
		/// [Get] Метод, регистрация нового пользователя 
		/// </summary>
		[Route("Register")]
		[HttpGet]
		public IActionResult Register()
		{
			var model=_userService.Register();
			return View("Register",model);
		}
		/// <summary>
		/// [Post] Метод, регистрация нового пользователя 
		/// </summary>
		[Route("Register")]
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				await _userService.Register(model);
				_logger.LogInformation($"Пользователь с логином {model.EmailReg} зарегистрирован");
				return RedirectToAction("Index", "Home");
			}
			else
			{
				foreach (var item in ModelState)
				{
					foreach (var error in item.Value.Errors)
					{
						Console.WriteLine(error.ErrorMessage);
					}
				}
			}
			return View("Register");
		}
		/// <summary>
		/// [Post] Метод, регистрация нового пользователя (только для администратора)
		/// </summary>
		[Authorize ("OnlyUberAdmin")]
		[Route("AdminRegister")]
		[HttpPost]
		public async Task<IActionResult> AdminRegister(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result=await _userService.AdminRegister(model);
				if(result.Succeeded)
				{
					_logger.LogInformation($"Пользователь с логином {model.EmailReg} зарегистрирован");
					return RedirectToAction("Index", "Home");
				}
				else View("Register");
			}
			else
			{
				foreach (var item in ModelState)
				{
					foreach (var error in item.Value.Errors)
					{
						Console.WriteLine(error.ErrorMessage);
					}
				}
			}
			return View("Register");
		}
		/// <summary>
		/// [Post] Метод, добавление супер пользователя
		/// </summary>
		//[Route("AddUberAdmin")]
		//public IActionResult AddAdmin()
		//{
		//	var result=_userService.AddAdmin();
		//	if (result.Result.Succeeded == true)
		//	{
		//		return Ok("UberAdmin successfully created");
		//	}
		//	else
		//	{
		//		var errors = result.Result.Errors.ToList();
		//		foreach (var error in errors)
		//		{
		//			Console.WriteLine($"{error.Code}{error.Description}");
		//		}
		//		return Ok($"UberAdmin does't created");
		//	}


		//}
	}
}
