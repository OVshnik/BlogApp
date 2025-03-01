using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.Extensions;
using BlogApp.API.ViewModels.Roles;
using BlogApp.API.ViewModels.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Caching.Memory;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;
using System.Linq;
using BlogApp.API.Exceptions;

namespace BlogApp.API.Services;

public class UserService : IUserService
{
	private readonly UserManager<User> _userManager;
	private readonly SignInManager<User> _signInManager;
	private readonly RoleManager<Role> _roleManager;
	private readonly IMapper _mapper;
	private readonly IArticleRepository _articleRepository;
	private readonly ICommentRepository _commentRepository;
	public UserService(UserManager<User> userManager, SignInManager<User> signInManager,
		RoleManager<Role> roleManager, IMapper mapper,
		IArticleRepository articleRepository, ICommentRepository commentRepository)
	{
		_userManager = userManager;
		_signInManager = signInManager;
		_roleManager = roleManager;
		_mapper = mapper;
		_articleRepository = articleRepository;
		_commentRepository = commentRepository;
	}
	/// <summary>
	/// Метод для регистрации пользователя
	/// </summary>
	public async Task<IdentityResult> Register(RegisterViewModel model)
	{
		var user = _mapper.Map<User>(model);
		var result = await _userManager.CreateAsync(user, model.PasswordReg);
		if (result.Succeeded)
		{
			await _signInManager.SignInAsync(user, false);

			await _userManager.AddToRoleAsync(user, "User");
			return result;
		}
		return result;
	}
	/// <summary>
	/// Метод для регистрации пользователя с правами администратора
	/// </summary>
	public async Task<IdentityResult> AdminRegister(RegisterViewModel model)
	{
		var user = _mapper.Map<User>(model);
		var result = await _userManager.CreateAsync(user, model.PasswordReg);
		if (result.Succeeded)
		{
			var roleNames = model.Roles.Select(x => x.Name).ToList();
			var dbRoles = _roleManager.Roles.Where(n => roleNames.Contains(n.Name)).ToList();

			foreach (var role in dbRoles)
			{
				var roleName = role.Name;
				if (roleName != null)
					await _userManager.AddToRoleAsync(user, roleName);
			}
			return result;
		}
		return result;
	}
	/// <summary>
	/// Метод для входа в систему
	/// </summary>
	public async Task<SignInResult> Login(LoginViewModel model)
	{
		var user = await _userManager.FindByEmailAsync(model.Email);
		if (user != null)
		{
			var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
			if (result.Succeeded)
			{
				if(user.Email != null)
				await _userManager.AddClaimAsync(user, new Claim("Email", user.Email));
				return result;
			}
		}
		return SignInResult.Failed;
	}
	/// <summary>
	/// Метод для создания обновления данных пользователя
	/// </summary>
	public async Task<IdentityResult> UpdateUserAsync(UserEditViewModel model)
	{
		var user = await _userManager.FindByIdAsync(model.UserId);
		if (user != null)
		{
			user.Convert(model);
			var userRoles = await _userManager.GetRolesAsync(user);
			if (model.Roles.Count > 0)
			{
				foreach (var role in model.Roles)
				{
					if(role.Name!=null)
					await _userManager.AddToRoleAsync(user, role.Name);
				}
				foreach(var r in userRoles)
				{
					var mr = model.Roles.Select(x => x.Name).ToList();
					if (!mr.Contains(r))
						await _userManager.RemoveFromRoleAsync(user, r);
				}
			}
			var result = await _userManager.UpdateAsync(user);
			return result;
		}
		throw new ModelNotFoundException($"Пользователя с id={model.UserId} не удалось получить из БД");
	}
	/// <summary>
	/// Метод для получения пользователя
	/// </summary>
	public async Task<UserViewModel> GetUserAsync(string id)
	{
		var user = await _userManager.FindByIdAsync(id);
		if (user != null)
		{
			var model = _mapper.Map<UserViewModel>(user);

			var roles = await _userManager.GetRolesAsync(user);

			var allArticles = await _articleRepository.GetAllArticlesAsync();
			var articles = allArticles.Where(x => x.AuthorId == user.Id).ToList();

			var allComments = await _commentRepository.GetAllCommentsAsync();
			var comments = allComments.Where(c => c.CommentMakerId == user.Id).ToList();

			if (roles != null)
				model.Roles.AddRange(roles);
			if (articles != null)
				model.Articles.AddRange(articles);
			if (comments != null)
				model.Comments.AddRange(comments);

			return model;
		}
		throw new ModelNotFoundException($"Пользователя с id={id} не удалось получить из БД");
	}
	/// <summary>
	/// Метод для получения текущего пользователя 
	/// </summary>
	public async Task<UserViewModel> GetCurrentUserAsync(ClaimsPrincipal claims)
	{
		var user = await _userManager.GetUserAsync(claims);
		if (user != null)
		{
			var model = _mapper.Map<UserViewModel>(user);

			var roles = await _userManager.GetRolesAsync(user);

			var allArticles = await _articleRepository.GetAllArticlesAsync();
			var articles = allArticles.Where(x => x.AuthorId == user.Id).ToList();

			var allComments = await _commentRepository.GetAllCommentsAsync();
			var comments = allComments.Where(c => c.CommentMakerId == user.Id).ToList();

			if (roles != null)
				model.Roles.AddRange(roles);
			if (articles != null)
				model.Articles.AddRange(articles);
			if (comments != null)
				model.Comments.AddRange(comments);

			return model;
		}
		throw new ModelNotFoundException($"Пользователя с именем {claims.Identity?.Name} не удалось получить из БД");
	}
	/// <summary>
	/// Метод для получения всех пользователей
	/// </summary>
	public async Task<UserListViewModel> GetAllUsersAsync()
	{
		var users = await _userManager.Users.AsQueryable().Include(x => x.Articles).ToListAsync();

		var userList = new UserListViewModel() { };
		if (users != null)
		{
			userList.Users.AddRange(users);
		}
		return userList;
	}
	/// <summary>
	/// Метод для удаления пользователя
	/// </summary>
	public async Task<IdentityResult> DeleteUserAsync(string id)
	{
		var delUser = await _userManager.FindByIdAsync(id);
		if (delUser != null)
		{
			var result = await _userManager.DeleteAsync(delUser);
			return result;
		}
		throw new ModelNotFoundException($"Пользователя с id={id} не удалось получить из БД");
	}
}
