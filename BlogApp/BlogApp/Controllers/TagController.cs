using AutoMapper;
using Azure;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.ArticlesTags.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Formats.Tar;
using System.Linq.Expressions;

namespace BlogApp.Controllers
{
    public class TagController : Controller
	{
		private readonly IMapper _mapper;
		private readonly TagService _tagService;
		private readonly ArticleService _articleService;
		private readonly UserService _userService;
		public TagController(IMapper mapper, TagService tagService, ArticleService articleService, UserService userService)
		{
			_mapper = mapper;
			_tagService = tagService;
			_articleService = articleService;
			_userService = userService;
		}
		[Authorize("OnlyUser&Admin")]
		[Route("CreateTag")]
		[HttpGet]
		public IActionResult CreateTag(Guid articleId)
		{
			var newTag = new CreateTagViewModel()
			{
				ArticleId = articleId,
			};
			return View("AddTag", newTag);
		}
		[Authorize("OnlyUser&Admin")]
		[Route("CreateTag")]
		[HttpPost]
		public async Task<IActionResult> CreateTag(CreateTagViewModel model)
		{
			if (ModelState.IsValid)
			{
				var tag = _mapper.Map<Tag>(model);

				await _tagService.CreateNewTagAsync(tag);

				return RedirectToPage("/CreateTag");
			}
			return RedirectToPage("/TagsList");

		}
		[Authorize("OnlyUser&Admin")]
		[Route("GetTag")]
		[HttpGet]
		public async Task<IActionResult> GetTag(Guid id)
		{
			var findTag = await _tagService.GetTagByIdAsync(id);
			if (findTag != null)
			{
				var tag = _mapper.Map<TagViewModel>(findTag);

				return Ok(tag);
			}
			return Ok();
		}
		[Authorize("OnlyUser&Admin")]
		[Route("TagsList")]
		[HttpGet]
		public async Task<IActionResult> GetTags()
		{
			var findTags = await _tagService.GetAllTagsAsync();
			if (findTags != null)
			{
				var tags = new ListTagsViewModel();
				foreach (var tag in findTags)
				{
					tags.Tags.Add(new TagViewModel(tag));
				}
				return View("TagsList", tags);
			}
			return RedirectToPage("AddTag");
		}
		[Authorize("OnlyUser&Admin")]
		[Route("EditTag")]
		[HttpGet]
		public async Task<IActionResult> EditTag(Guid id)
		{
			var tag = await _tagService.GetTagByIdAsync(id);
			if (tag != null)
			{
				var edTag = _mapper.Map<EditTagViewModel>(tag);
				if (User.IsInRole("User"))
				{
					var user = await _userService.GetCurrentUserAsync(User);
					var articles = await _articleService.GetAllArticlesByAuthorIdAsync(user);

					if (articles.Where(x => x.Tags.Contains(tag)).FirstOrDefault() != null)
					{
						return Ok(edTag);
					}
					return Ok();
				}
				else
				{
					return Ok(edTag);
				}
			}
			return Ok();
		}
		[Authorize("OnlyUser&Admin")]
		[Route("UpdateTag")]
		[HttpPost]
		public async Task<IActionResult> UpdateTag(EditTagViewModel model)
		{
			var tag = _mapper.Map<Tag>(model);
			await _tagService.UpdateTagAsync(tag);
			return Ok(model);
		}
		[Authorize("OnlyUser&Admin")]
		[Route("DeleteTag")]
		[HttpGet]
		public async Task<IActionResult> DeleteTag(Guid id)
		{
			var tag = await _tagService.GetTagByIdAsync(id);
			if (tag != null)
			{
				await _tagService.DeleteTagAsync(tag);
				return Ok();
			}
			return Ok();
		}
		[AcceptVerbs("Get", "Post")]
		public async Task<IActionResult> CheckTagName(string name)
		{
			var tags = await _tagService.GetAllTagsAsync();
			if (!tags.Where(x => x.Name == name).Any())
			{
				return Json(true);
			}
			return Json(false);
		}
	}
}
