using BlogApp.Data.Models;
using BlogApp.ViewModels.Comments;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.Services;

public interface ICommentService
{
	public Task<Guid> CreateNewCommentAsync(ClaimsPrincipal claims, CreateCommentViewModel model);
	public Task<CommentViewModel> GetCommentAsync(Guid id);
	public Task<CommentListViewModel> GetAllCommentsAsync();
	public Task<EditCommentViewModel> EditComment(Guid id);
	public Task UpdateCommentAsync(EditCommentViewModel model);
	public Task DeleteCommentAsync(Guid id);
}
