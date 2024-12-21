using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Services;
using BlogApp.ViewModels.ArticlesTags;
using BlogApp.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
	public class CommentController : Controller
	{
		private readonly IMapper _mapper;
		private readonly CommentService _commentService;
		private readonly UserService _userService;

		public CommentController(IMapper mapper, CommentService commentService, UserService userService)
		{
			_mapper = mapper;
			_commentService = commentService;
			_userService = userService;
		}
		[Authorize]
		[HttpGet]
		[Route("AddComment")]
		public IActionResult CreateComment(Guid articleId)
		{
			var model = new CreateCommentViewModel()
			{
				ArticleId = articleId,
			};
			return Ok(model);
		}
		[Authorize]
		[HttpPost]
		[Route("AddComment")]
		public async Task<IActionResult> CreateComment(CreateCommentViewModel model)
		{
			var currentUser = await _userService.GetCurrentUserAsync(User);
			if (currentUser != null)
			{
				var newComment = _mapper.Map<Comment>(model);

				newComment.CommentMakerId = currentUser.Id;

				await _commentService.CreateNewCommentAsync(newComment);

				return Ok("New comment added");
			}
			return Ok("Comment not added");
		}
		[Authorize]
		[HttpGet]
		[Route("GetComment")]
		public async Task<IActionResult> GetComment(Guid id)
		{
			var findComment = await _commentService.GetCommentByIdAsync(id);
			if (findComment != null)
			{
				var comment = _mapper.Map<CommentViewModel>(findComment);

				return Ok(comment);
			}
			return Ok();
		}
		[Authorize]
		[HttpGet]
		[Route("GetCommentsByArticle")]
		public async Task<IActionResult> GetCommentsByArticle(Guid articleId)
		{
			var result = new CommentListViewModel();

			var comments = await _commentService.GetCommentsByArticleAsync(articleId);

			if (comments != null)
			{
				result.Comments.AddRange(comments);

				return Ok(result);
			}
			return Ok();
		}
		[Authorize]
		[HttpGet]
		[Route("GetCommentsByUser")]
		public async Task<IActionResult> GetCommentsByUser()
		{
			var currentUser = await _userService.GetCurrentUserAsync(User);

			var result = new CommentListViewModel();

			var comments = await _commentService.GetCommentByUserAsync(currentUser);

			if (comments != null)
			{
				result.Comments.AddRange(comments);

				return Ok(comments);
			}
			return Ok();
		}
		[Authorize]
		[HttpGet]
		[Route("EditComment")]
		public async Task<IActionResult> ChangeComment(Guid id)
		{
			var comment = await _commentService.GetCommentByIdAsync(id);
			if (comment != null)
			{
				var editModel = _mapper.Map<EditCommentViewModel>(comment);

				if (User.IsInRole("User"))
				{
					var user = await _userService.GetCurrentUserAsync(User);
					if (user.Id == comment.CommentMakerId)
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
		[HttpPost]
		[Route("UpdateComment")]
		public async Task<IActionResult> UpdateComment(EditCommentViewModel model)
		{
			var comment = await _commentService.GetCommentByIdAsync(model.Id);
			if (comment != null)
			{
				comment.Content = model.Content;
				await _commentService.UpdateCommentAsync(comment);
			}
			return Ok();
		}
		[Authorize]
		[HttpPost]
		[Route("DeleteComment")]
		public async Task<IActionResult> DeleteComment(Guid id)
		{
			var comment = await _commentService.GetCommentByIdAsync(id);
			if (comment != null)
			{

				if (User.IsInRole("User"))
				{
					var user = await _userService.GetCurrentUserAsync(User);
					if (user.Id == comment.CommentMakerId)
					{
						await _commentService.DeleteCommentAsync(comment);
						return Ok();
					}
					return Ok();
				}
				else
				{
					await _commentService.DeleteCommentAsync(comment);
					return Ok();
				}
			}
			return Ok();
		}
	}
}
