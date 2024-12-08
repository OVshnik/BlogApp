using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.Articles;
using BlogApp.ViewModels.Tags;
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
			return Ok(newTag);
		}
		[Authorize("OnlyUser&Admin")]
		[Route("CreateTag")]
		[HttpPost]
		public async Task<IActionResult> CreateTag(CreateTagViewModel model)
		{
			var tag = _mapper.Map<Tag>(model);

			await _tagService.CreateNewTagAsync(tag);

			return Ok("Tag added");
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
		[Route("GetAllTags")]
		[HttpGet]
		public async Task<IActionResult> GetTags()
		{
			var findTags = await _tagService.GetAllTagsAsync();
			if (findTags != null)
			{
				var tags = new ListTagsViewModel()
				{
					Tags = findTags
				};
				return Ok(tags);
			}
			return Ok();
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

	}
}
