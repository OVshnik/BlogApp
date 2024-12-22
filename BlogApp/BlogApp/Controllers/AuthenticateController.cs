using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.UsersRoles.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class AuthenticateController : Controller
	{
		private readonly IMapper _mapper;
		private readonly SignInManager<User> _signInManager;
		public AuthenticateController(SignInManager<User> signInManager, IMapper mapper)
		{
			_signInManager = signInManager;
			_mapper = mapper;
		}
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
        [HttpGet]
		public IActionResult Login(string ?returnUrl="")
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl });
		}
		[Route("Login")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{ 

				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);

				if (result.Succeeded)
				{
					var user = _mapper.Map<User>(model);
					await AddClaims(user);
					return Ok("User successfully login");
				}
				else
				{
					ModelState.AddModelError("", "Неправильный логин и (или) пароль");
				}
			}
			foreach (var item in ModelState)
			{
				foreach (var error in item.Value.Errors)
				{
					Console.WriteLine(error.ErrorMessage);

				}
			}
			return Ok("User doesn't login");

		}
		[Route("Logout")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return Ok("User is logout");
		}
		public async Task AddClaims(User user)
		{
			var claims = new List<Claim>();
			var userRoles = await _signInManager.UserManager.GetRolesAsync(user);

			foreach (var userRole in userRoles)
			{
				claims.Add(new Claim("Role",userRole));
			}
			 
			await _signInManager.UserManager.AddClaimsAsync(user,claims);
		}
	}
}
