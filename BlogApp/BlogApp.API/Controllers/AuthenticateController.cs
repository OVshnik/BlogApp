using AutoMapper;
using BlogApp.API.Services;
using BlogApp.API.ViewModels.Users;
using BlogApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticateController : ControllerBase
	{
		private readonly IUserService _userService;
		public AuthenticateController(IUserService userService)
		{
			_userService = userService;
		}
		[Route("Login")]
		[HttpPost]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _userService.Login(model);
				if (result.Succeeded)
				{
					return StatusCode(200);
				}
				else
				{
					return StatusCode(501);
				}
			}
			return BadRequest("Указанные данные не прошли валидацию");
		}
	}
}
