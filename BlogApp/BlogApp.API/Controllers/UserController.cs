using BlogApp.API.Services;
using BlogApp.API.ViewModels.Users;
using BlogApp.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;

namespace BlogApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly IUserService _userService;
	public UserController (IUserService userService,UserManager<User> userManager)
	{
		_userService = userService;
	}
	[Authorize("OnlyAdmin")]
	[HttpGet]
	[Route("GetUser")]
	public async Task<IActionResult> GetUser(string id)
	{
		var model = await _userService.GetUserAsync(id);
		if(model==null)
		{
			return NotFound();
		}
		return StatusCode(200,model);
	}
	[Authorize("OnlyAdmin")]
	[HttpGet]
	[Route("GetUsers")]
	public async Task<IActionResult> GetUsers()
	{
		var model = await _userService.GetAllUsersAsync();
		if (model==null)
		{
			return NotFound();
		}
		return StatusCode(200, model);
	}
	[Authorize("OnlyAdmin")]
	[HttpPatch]
	[Route("EditUser")]
	public async Task<IActionResult> EditUser(UserEditViewModel model)
	{
		if (ModelState.IsValid)
		{
			var result=await _userService.UpdateUserAsync(model);
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
	[Route("DeleteUser")]
	public async Task<IActionResult> DeleteUser(string id)
	{
		var model = await _userService.GetAllUsersAsync();
		if (model != null)
		{
			return NotFound();
		}
		return StatusCode(200, model);
	}

}
