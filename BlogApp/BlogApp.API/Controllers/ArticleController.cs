using BlogApp.API.Services;
using BlogApp.API.ViewModels.Articles;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ArticleController : ControllerBase
	{
		private readonly IArticleService _articleService;
		public ArticleController(IArticleService articleService)
		{
			_articleService = articleService;
		}
		[Authorize("OnlyAdmin")]
		[Route("AddArticle")]
		[HttpPost]
		public async Task<IActionResult> CreateArticle(CreateArticleViewModel model)
		{
			if (ModelState.IsValid)
			{
				await _articleService.CreateNewArticleAsync(model);

				return StatusCode(201);
			}
			else
			{
				return BadRequest("Указанные данные не прошли валидацию");
			}
		}
		[Authorize("OnlyAdmin")]
		[Route("GetArticle")]
		[HttpGet]
		public async Task<IActionResult> GetArticle(Guid id)
		{
			var article=await _articleService.GetArticleAsync(id);
			if (article != null)
			{
				return StatusCode(200,article);
			}
			return NotFound();
		}
		[Authorize("OnlyAdmin")]
		[Route("GetAllArticles")]
		[HttpGet]
		public async Task<IActionResult> GetAllArticles()
		{
			var articles = await _articleService.GetAllArticlesAsync();
			if (articles != null)
			{
				return StatusCode(200, articles);
			}
			return NotFound();
		}
		[Authorize("OnlyAdmin")]
		[Route("EditArticle")]
		[HttpPatch]
		public async Task<IActionResult> EditArticle(EditArticleViewModel model)
		{
			if (ModelState.IsValid)
			{
				await _articleService.UpdateArticleAsync(model);
				return StatusCode(200);
			}
			return BadRequest("Указанные данные не прошли валидацию");
		}
		[Authorize("OnlyAdmin")]
		[Route("DeleteArticle")]
		[HttpDelete]
		public async Task<IActionResult> DeleteArticle(Guid id)
		{
			await _articleService.DeleteArticleAsync(id);

            return StatusCode(200);
		}
	}
}
