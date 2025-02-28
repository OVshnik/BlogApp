using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.Articles;
using BlogApp.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogApp.Controllers
{
    public class CommentController : Controller
	{
		private readonly IMapper _mapper;
		private readonly ICommentService _commentService;
		private readonly IUserService _userService;
		private readonly ILogger<CommentController> _logger;


		public CommentController(IMapper mapper, ICommentService commentService, IUserService userService, ILogger<CommentController> logger)
		{
			_commentService = commentService;			
			_mapper = mapper;
			_logger= logger;
			_userService = userService;
		}
		/// <summary>
		/// [Post] Метод, добавление комментария
		/// </summary>
		[Authorize]
		[HttpPost]
		[Route("AddComment")]
		public async Task<IActionResult> CreateComment(ArticleViewModel model)
		{
			var id= await _commentService.CreateNewCommentAsync(User, model.CreateComment);
			_logger.LogInformation($"К статье с id={model.Id} добавлен комментарий с id={id}");
			return RedirectToAction("GetAllArticle", "Article");
		}
		/// <summary>
		/// [Get] Метод, страница с комментарием
		/// </summary>
		[Authorize]
		[HttpGet]
		[Route("CommentPage")]
		public async Task<IActionResult> GetComment(Guid id)
		{
			var model=await _commentService.GetCommentAsync(id);
			return View("CommentPage",model);
		}
		/// <summary>
		/// [Get] Метод, список комментариев
		/// </summary>
		[Authorize]
		[HttpGet]
		[Route("CommentsList")]
		public async Task<IActionResult> GetAllComments()
		{
			var model=await _commentService.GetAllCommentsAsync();
			return View("CommentsList",model);
		}
		/// <summary>
		/// [Post] Метод, редактирования комментария
		/// </summary>
		[Authorize]
		[HttpPost]
		[Route("EditComment")]
		public async Task<IActionResult> EditComment(Guid id)
		{
			var model=await _commentService.EditComment(id);
			return View(model);
		}
		/// <summary>
		/// [Post] Метод, обновление комментария
		/// </summary>
		[Authorize]
		[HttpPost]
		[Route("UpdateComment")]
		public async Task<IActionResult> UpdateComment(EditCommentViewModel model)
		{
			if (ModelState.IsValid)
			{
				await _commentService.UpdateCommentAsync(model);
				_logger.LogDebug($"Комментарий с id={model.Id} изменен");
				return RedirectToAction("GetAllComments");
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
				return RedirectToAction("EditComment");
			}

		}
		/// <summary>
		/// [Post] Метод, удаление комментария
		/// </summary>
		[Authorize]
		[HttpPost]
		[Route("DeleteComment")]
		public async Task<IActionResult> DeleteComment(Guid id)
		{
			await _commentService.DeleteCommentAsync(id);
			_logger.LogDebug($"Комментарий с id={id} удален");
			return RedirectToAction("GetAllComments");
		}
	}
}
