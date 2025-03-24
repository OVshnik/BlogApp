using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.Articles;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogApp.Controllers;

public class ArticleController : Controller
{
	private readonly IMapper _mapper;
	private readonly IArticleService _articleService;
	private readonly IUserService _userService;
	private readonly ITagService _tagService;
	private readonly UserManager<User> _userManager;
	public ArticleController(IMapper mapper, IArticleService articleService, IUserService userService, ITagService tagService, UserManager<User> userManager)
	{
		_mapper = mapper;
		_articleService = articleService;
		_userService = userService;
		_tagService = tagService;
		_userManager = userManager;
	}

	/// <summary>
	/// [Get] Метод, добавление статьи
	/// </summary>
	[Authorize]
	[Route("AddArticle")]
	[HttpGet]
	public async Task<IActionResult> CreateArticle()
	{
		var user = User;
		if (user != null)
		{
			var model = await _articleService.CreateNewArticleAsync(user);
			return View("AddArticle", model);
		}
		return RedirectToAction("Login");
	}

	/// <summary>
	/// [Post] Метод, добавление статьи
	/// </summary>
	[Authorize]
	[Route("AddArticle")]
	[HttpPost]
	public async Task<IActionResult> CreateArticle(CreateArticleViewModel model)
	{
		var AddArticleId = await _articleService.CreateNewArticleAsync(model);

		return RedirectToAction("GetAllArticle");
	}

	/// <summary>
	/// [Get] Метод, список статей
	/// </summary>
	[Authorize]
	[Route("ArticlesList")]
	[HttpGet]
	public async Task<IActionResult> GetAllArticle()
	{
		var articles = await _articleService.GetAllArticlesAsync();
		if (articles != null)
		{
			return View("ArticlesList", articles);
		}
		return RedirectToAction("CreateArticle");
	}

	/// <summary>
	/// [Get] Метод, страница статьи
	/// </summary>
	[Authorize]
	[Route("GetArticle")]
	[HttpGet]
	public async Task<IActionResult> GetArticle(Guid id)
	{
		var model = await _articleService.GetArticleAsync(id);

		return View("ArticlePage", model);
	}

	/// <summary>
	/// [Post] Метод, редактирование статьи
	/// </summary>
	[Authorize]
	[Route("EditArticle")]
	[HttpPost]
	public async Task<IActionResult> EditArticle(Guid id)
	{
		var model = await _articleService.EditArticleAsync(id);
		return View(model);
	}

	/// <summary>
	/// [Post] Метод, обновление статьи
	/// </summary>
	[Authorize]
	[Route("UpdateArticle")]
	[HttpPost]
	public async Task<IActionResult> UpdateArticle(EditArticleViewModel model)
	{
		if (ModelState.IsValid)
		{
			await _articleService.UpdateArticleAsync(model);

			return RedirectToAction("GetAllArticle");
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
			return RedirectToAction("EditArticle");
		}
	}

	/// <summary>
	/// [Post] Метод, удаление статьи
	/// </summary>
	[Authorize]
	[Route("DeleteArticle")]
	[HttpPost]
	public async Task<IActionResult> DeleteArticle(Guid id)
	{
		await _articleService.DeleteArticleAsync(id);
		return RedirectToAction("GetAllArticle");
	}
}
