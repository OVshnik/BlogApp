using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Extensions;
using BlogApp.Services;
using BlogApp.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BlogApp.Controllers
{
    public class UserController : Controller
	{
		private readonly IMapper _mapper;
		private readonly UserService _userService;
		public UserController(UserService userService, IMapper mapper)
		{
			_userService = userService;
			_mapper = mapper;
		}
		[Authorize]
		[HttpGet]
		public IActionResult UserPage()
		{
			return View();
		}
		[Authorize("OnlyForAdmin")]
		[Route("GetUser")]
		[HttpGet]
		public async Task<IActionResult> GetUsersById(string id)
		{
			var user=await _userService.GetUserByIdAsync(id);
            if (user!=null)
            {
				var model = _mapper.Map<UserViewModel>(user);
				return Ok(model);
			}
			return Ok("User not found");
		}
		[Authorize("OnlyForAdmin")]
		[Route("GetUsers")]
		[HttpGet]
		public async Task<IActionResult>GetAllUsers()
		{
			var users = await _userService.GetAllUsersAsync();
			var userList = new List<UserViewModel>();
			foreach (var user in users)
			{
				var mapUser=_mapper.Map<UserViewModel>(user);
				userList.Add(mapUser);
			}
			return Ok(userList);
		}
		[Route("EditUser")]
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> EditUser()
		{
			var user = User;

			await _userService.GetCurrentUserAsync(user);

			var model=_mapper.Map<UserEditViewModel>(user);

			return Ok(model);
		}
		[Route("AdminEditUser")]
		[Authorize("OnlyAdmin")]
		[HttpGet]
		public async Task<IActionResult> EditUser(string id)
		{
			var user=await _userService.GetUserByIdAsync(id);

			var model = _mapper.Map<UserEditViewModel>(user);

			return Ok(model);
		}
		[Route("UpdateUser")]
		[Authorize]
		[HttpGet]
		public async Task<IActionResult>UpdateUser(UserEditViewModel? model)
		{
			if (ModelState.IsValid)
			{
				var user = await _userService.GetUserByIdAsync(model.UserId);
				user.Convert(model);
				await _userService.UpdateUserAsync(user);
				return Ok("User successfully updated");
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
				return Ok("One or more input values is invalid");
			}
		}
		[Authorize("OnlyAdmin")]
		[Route("DeleteUser")]
		[HttpPost]
		public async Task<IActionResult> DeleteUser(string id)
		{
			await _userService.DeleteUserAsync(id);
			return Ok("User has been deleted");
		}
	}
}
