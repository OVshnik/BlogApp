using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.Articles;
using BlogApp.ViewModels.Tags;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
	public class ArticleController : Controller
	{
		private readonly IMapper _mapper;
		private readonly ArticleService _articleService;
		private readonly UserService _userService;
		public ArticleController(IMapper mapper, ArticleService articleService, UserService userService)
		{
			_mapper = mapper;
			_articleService = articleService;
			_userService = userService;
		}
		[Authorize]
		[Route("CreateArticle")]
		[HttpGet]
		public IActionResult CreateArticle()
		{
			var newArticle = new CreateArticleViewModel();
			return Ok(newArticle);
		}
		[Authorize]
		[Route("CreateArticle")]
		[HttpPost]
		public async Task<IActionResult> CreateArticle(CreateArticleViewModel model)
		{
			var currentUser = await _userService.GetCurrentUserAsync(User);

			if (model != null&&currentUser!=null)
			{
				var article = _mapper.Map<Article>(model);

				article.AuthorId = currentUser.Id;

				await _articleService.CreateNewArticleAsync(article);
				return Ok("Article has been added");
			}
			return Ok("Article doesn't added");
		}
		[Authorize]
		[Route("GetAllArticles")]
		[HttpGet]
		public async Task<IActionResult> GetAllArticle()
		{
			var articles = await _articleService.GetAllArticlesAsync();
			if (articles != null)
			{
				var articleList = new ArticleListViewModel();

				articleList.Articles.AddRange(articles);

				var result = articleList.Articles.OrderBy(x => x.Author.LastName).ToList();

				return Ok(result);
			}
			return View();
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
