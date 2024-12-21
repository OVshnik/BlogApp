using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
	public class RegisterController : Controller
	{
		private readonly IMapper _mapper;
		private readonly UserService _userService;
		private readonly SignInManager<User> _signInManager;
		public RegisterController(UserService userService, SignInManager<User> signInManager, IMapper mapper)
		{
			_userService = userService;
			_signInManager = signInManager;
			_mapper = mapper;
		}
		[Route("Register")]
		[HttpGet]
		public IActionResult Register()
		{
			var model = new RegisterViewModel();
			return View("Register",model);
		}
		[Route("Register")]
		[HttpPost]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				var user = _mapper.Map<User>(model);
				var result = await _userService.AddNewUserAsync(user, model.PasswordReg);
				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, false);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
					}
				}
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
	}
}
