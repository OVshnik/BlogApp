using BlogApp.API.Services;
using BlogApp.API.ViewModels.Articles;
using BlogApp.API.ViewModels.Comments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace BlogApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CommentController : ControllerBase
{
	private readonly ICommentService _commentService;
	public CommentController(ICommentService commentService)
	{
		_commentService = commentService;
	}
	[Authorize("OnlyAdmin")]
	[HttpPost]
	[Route("AddComment")]
	public async Task<IActionResult> CreateComment(CreateCommentViewModel model)
	{
		if (ModelState.IsValid)
		{
			await _commentService.CreateNewCommentAsync(model);
			return StatusCode(201);
		}
		return BadRequest(ModelState);
	}
	[Authorize("OnlyAdmin")]
	[HttpGet]
	[Route("GetComment")]
	public async Task<IActionResult> GetComment(Guid id)
	{
		var model=await _commentService.GetCommentAsync(id);
		return StatusCode(200,model);
	}
	[Authorize("OnlyAdmin")]
	[HttpGet]
	[Route("GetAllComments")]
	public async Task<IActionResult> GetAllComments()
	{
		var comments = await _commentService.GetAllCommentsAsync();
		return StatusCode(200, comments);
	}
	[Authorize("OnlyAdmin")]
	[HttpPatch]
	[Route("EditComment")]
	public async Task<IActionResult> EditComment(EditCommentViewModel model)
	{
		if(ModelState.IsValid)
		{
			await _commentService.UpdateCommentAsync(model);
			return StatusCode(201);
		}
		return BadRequest(ModelState);
	}
	[Authorize("OnlyAdmin")]
	[HttpDelete]
	[Route("DeleteComment")]
	public async Task<IActionResult> DeleteComment(Guid id)
	{
		await _commentService.DeleteCommentAsync(id);
		return StatusCode(201);
	}
}
