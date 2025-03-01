using BlogApp.API.Services;
using BlogApp.API.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegisterController : ControllerBase
{
	private readonly IUserService _userService;
	public RegisterController(IUserService userService)
	{
		_userService = userService;
	}
	[Authorize("OnlyAdmin")]
	[HttpPost]
	[Route("AddUser")]
	public async Task<IActionResult> Register(RegisterViewModel model)
	{
		if (ModelState.IsValid)
		{
			var result=await _userService.AdminRegister(model);
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
}
