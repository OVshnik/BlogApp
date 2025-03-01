using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;
using System.Linq.Expressions;

namespace BlogApp.Controllers;

    public class TagController : Controller
{
	private readonly IMapper _mapper;
	private readonly ITagService _tagService;
	private readonly IArticleService _articleService;
	private readonly IUserService _userService;
	private ILogger<TagController> _logger;
	public TagController(IMapper mapper, ITagService tagService, IArticleService articleService, IUserService userService, ILogger<TagController> logger)
	{	
		_mapper = mapper;
		_tagService = tagService;
		_articleService = articleService;
		_userService = userService;
		_logger = logger;

	}
	/// <summary>
	/// [Get] Метод, создание тега
	/// </summary>
	[Authorize]
	[Route("CreateTag")]
	[HttpGet]
	public IActionResult CreateTag()
	{
		return View("AddTag", new CreateTagViewModel());
	}
	/// <summary>
	/// [Post] Метод, создание тега
	/// </summary>
	[Authorize]
	[Route("CreateTag")]
	[HttpPost]
	public async Task<IActionResult> CreateTag(CreateTagViewModel model)
	{
		if (ModelState.IsValid)
		{
			await _tagService.CreateNewTagAsync(model);
			_logger.LogInformation($"Создан тег с именем {model.Name}");

			return RedirectToAction("GetTags");
		}
		return RedirectToAction("CreateTag");

	}
	/// <summary>
	/// [Get] Метод, страница тега
	/// </summary>
	[Authorize]
	[Route("TagPage")]
	[HttpGet]
	public async Task<IActionResult> GetTag(Guid id)
	{
		var model=await _tagService.GetTagAsync(id);
		return View("TagPage", model);
	}
	/// <summary>
	/// [Get] Метод, список тегов
	/// </summary>
	[Authorize]
	[Route("TagsList")]
	[HttpGet]
	public async Task<IActionResult> GetTags()
	{
		var tags=await _tagService.GetAllTagsAsync();

		if (tags.Tags.Count != 0)
		return View("TagsList", tags);

		return RedirectToPage("AddTag");
	}
	/// <summary>
	/// [Post] Метод, редактирование тега
	/// </summary>
	[Authorize("OnlyUser&Admin")]
	[Route("EditTag")]
	[HttpPost]
	public async Task<IActionResult> EditTag(Guid id)
	{
		var model = await _tagService.EditTag(id);
		return View(model);
	}
	/// <summary>
	/// [Post] Метод, обновление тега
	/// </summary>
	[Authorize("OnlyUser&Admin")]
	[Route("UpdateTag")]
	[HttpPost]
	public async Task<IActionResult> UpdateTag(EditTagViewModel model)
	{
		if (ModelState.IsValid)
		{
			await _tagService.UpdateTagAsync(model);
			_logger.LogDebug($"Данные тега с именем '{model.Name}' изменены");
			return RedirectToAction("GetTags");
		}
		return RedirectToAction("EditTag");
	}
	/// <summary>
	/// [Post] Метод, удаление тега
	/// </summary>
	[Authorize("OnlyUser&Admin")]
	[Route("DeleteTag")]
	[HttpPost]
	public async Task<IActionResult> DeleteTag(Guid id)
	{
		await _tagService.DeleteTagAsync(id);
		_logger.LogDebug($"Тег с id={id} удален");
		return RedirectToAction("GetTags");
	}
	/// <summary>
	/// Метод, проверка имени тега на уникальность
	/// </summary>
	[AcceptVerbs("Get", "Post")]
	public async Task<IActionResult> CheckTagName(string name)
	{
		var tags = await _tagService.GetAllTagsAsync();
		if (!tags.Tags.Where(x => x.Name == name).Any())
		{
			return Json(true);
		}
		return Json(false);
	}
}
