using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Extensions;
using BlogApp.Services;
using BlogApp.ViewModels.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.Controllers
{
    public class UserController : Controller
	{
		private readonly IMapper _mapper;
		private readonly IUserService _userService;
		private readonly RoleManager<Role> _roleManager;
		private readonly ILogger<UserController> _logger;
		public UserController(IUserService userService, IMapper mapper, RoleManager<Role> roleManager, ILogger<UserController> logger)
		{
			_userService = userService;
			_mapper = mapper;
			_roleManager = roleManager;
			_logger = logger;
		}
		/// <summary>
		/// [Get] Метод, страница пользователя
		/// </summary>
		[Authorize]
        [Route("MyPage")]
        [HttpGet]
        public async Task<IActionResult> MyPage()
        {
			var model =await _userService.GetCurrentUserAsync(User);

            return View("UserPage", model);
        }
		/// <summary>
		/// [Get] Метод, страница пользователя
		/// </summary>
		[Authorize]
		[Route("UserPage")]
		[HttpGet]
		public async Task<IActionResult> GetUser(string id)
		{
		    var model=await _userService.GetUserAsync(id);

			return View("UserPage",model);
		}
		/// <summary>
		/// [Get] Метод, список всех пользователей
		/// </summary>
		[Authorize]
		[Route("UserList")]
		[HttpGet]
		public async Task<IActionResult>GetAllUsers()
		{
			var users=await _userService.GetAllUsersAsync();
			return View("UserList",users);
		}
		/// <summary>
		/// [Get] Метод, редактирование пользователя
		/// </summary>
		[Route("EditUser")]
		[Authorize]
		[HttpGet]
		public async Task<IActionResult> EditUser()
		{
			var user = User;

			var model=await _userService.EditUser(user);

			return View("EditUser", model);
		}
		/// <summary>
		/// [Get] Метод, редактирование пользователя (только для администратора)
		/// </summary>
		[Route("AdminEditUser")]
		[Authorize("OnlyAdmin")]
		[HttpPost]
		public async Task<IActionResult> AdminEditUser(string id)
		{
			var model = await _userService.EditUser(id);

			return View("EditUser", model);
		}
		/// <summary>
		/// [Post] Метод, обновление данных пользователя
		/// </summary>
		[Route("UpdateUser")]
		[Authorize]
		[HttpPost]
		public async Task<IActionResult>UpdateUser(UserEditViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _userService.UpdateUserAsync(model);
				if (result.Succeeded)
				{
					_logger.LogDebug($"У пользователя с логином {model.Email} изменены данные");
					return RedirectToAction("GetAllUsers");
				}
				else
					return RedirectToAction("EditUser");
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
				return RedirectToAction("EditUser");
			}
		}
		/// <summary>
		/// [Post] Метод, удаление пользователя
		/// </summary>
		[Authorize("OnlyAdmin")]
		[Route("DeleteUser")]
		[HttpPost]
		public async Task<IActionResult> DeleteUser(string id)
		{
			var user=await _userService.GetUserAsync(id);
            await _userService.DeleteUserAsync(id);

			_logger.LogDebug($"Пользователь {user.User.Email} удален");
			return View("UserList");
		}
	}
}
