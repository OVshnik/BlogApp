using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.ArticlesTags.Articles;
using BlogApp.ViewModels.ArticlesTags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BlogApp.ViewModels.ArticlesTags.Tags;

namespace BlogApp.Controllers
{
	public class ArticleController : Controller
	{
		private readonly IMapper _mapper;
		private readonly ArticleService _articleService;
		private readonly UserService _userService;
		private readonly TagService _tagService;
		public ArticleController(IMapper mapper, ArticleService articleService, UserService userService, TagService tagService)
		{
			_mapper = mapper;
			_articleService = articleService;
			_userService = userService;
			_tagService = tagService;
		}
		[Authorize]
		[Route("AddArticle")]
		[HttpGet]
		public async Task<IActionResult> CreateArticle()
		{
			var newArticle = new ArticleTagViewModel();

			var tags = await _tagService.GetAllTagsAsync();

			foreach (var tag in tags)
			{
				newArticle.ListTags.Tags.Add(new TagViewModel(tag));
			}

			return View("AddArticle", newArticle);
		}
		[Authorize]
		[Route("AddArticle")]
		[HttpPost]
		public async Task<IActionResult> CreateArticle(ArticleTagViewModel model)
		{
			var currentUser = await _userService.GetCurrentUserAsync(User);

			if (model != null && currentUser != null)
			{
				var article = _mapper.Map<Article>(model.CreateArticle);

				article.AuthorId = currentUser.Id;

				await _articleService.CreateNewArticleAsync(article);

				return View("AddArticle");
			}
			return View("AddArticle");
		}
		[Authorize]
		[Route("ArticlesList")]
		[HttpGet]
		public async Task<IActionResult> GetAllArticle()
		{
			var articles = await _articleService.GetAllArticlesAsync();
			if (articles != null)
			{
				var articleList = new ArticleListViewModel();

				articleList.Articles.AddRange(articles);

				//var result = articleList.Articles.OrderBy(x => x.Author.LastName).ToList();

				return View("ArticlesList", articleList);
			}
			return RedirectToPage("/AddArticle");
		}
		[Authorize]
		[Route("GetArticlesByAuthor")]
		[HttpGet]
		public async Task<IActionResult> GetArticlesByAuthor()
		{
			var currentUser = await _userService.GetCurrentUserAsync(User);

			var articles = await _articleService.GetAllArticlesByAuthorIdAsync(currentUser);

			if (articles != null && currentUser != null)
			{
				var articleList = new ArticleListViewModel();

				articleList.Articles.AddRange(articles);

				var result = articleList.Articles.OrderBy(x => x.Title).ToList();

				return Ok(result);
			}
			return Ok();
		}
		[Authorize("OnlyModerator&Admin")]
		[Route("GetArticlesByAuthorId")]
		[HttpGet]
		public async Task<IActionResult> GetArticlesByAuthorId(string id)
		{
			var requiredUser = await _userService.GetUserByIdAsync(id);

			var articles = await _articleService.GetAllArticlesByAuthorIdAsync(requiredUser);

			if (articles != null && requiredUser != null)
			{
				var articleList = new ArticleListViewModel();

				articleList.Articles.AddRange(articles);

				var result = articleList.Articles.OrderBy(x => x.Title).ToList();

				return Ok(result);
			}
			return Ok("Articles not found");
		}
		[Authorize]
		[Route("EditArticle")]
		[HttpGet]
		public async Task<IActionResult> EditArticle(Guid id)
		{
			var article = await _articleService.GetArticleByIdAsync(id);

			if (article != null)
			{
				var editModel = _mapper.Map<EditArticleViewModel>(article);
				if (User.IsInRole("User"))
				{
					var user = await _userService.GetCurrentUserAsync(User);
					if (user.Id == article.AuthorId)
					{
						return Ok(editModel);
					}
					return Ok();
				}
				else
				{
					return Ok(editModel);
				}
			}
			return Ok();
		}
		[Authorize]
		[Route("UpdateArticle")]
		[HttpPost]
		public async Task<IActionResult> UpdateArticle(EditArticleViewModel? model)
		{
			var article = await _articleService.GetArticleByIdAsync(model.Id);

			await _articleService.UpdateArticleAsync(article);
			return Ok();
		}
		[Authorize]
		[Route("DeleteArticle")]
		[HttpPost]
		public async Task<IActionResult> DeleteArticle(Guid id)
		{
			var article = await _articleService.GetArticleByIdAsync(id);
			if (article != null)
			{
				if (User.IsInRole("User"))
				{
					var user = await _userService.GetCurrentUserAsync(User);
					if (user.Id == article.AuthorId)
					{
						await _articleService.DeleteArticleAsync(article);
						return Ok();
					}
					return Ok();
				}
				else
				{
					await _articleService.DeleteArticleAsync(article);
					return Ok();
				}
			}
			return Ok();
		}
	}
}
