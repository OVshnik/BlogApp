using BlogApp.Data.Models;
using BlogApp.API.ViewModels.Comments;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.API.Services
{
	public interface ICommentService
	{
		public Task CreateNewCommentAsync(CreateCommentViewModel model);
		public Task<CommentViewModel> GetCommentAsync(Guid id);
		public Task<CommentListViewModel> GetAllCommentsAsync();
		public Task UpdateCommentAsync(EditCommentViewModel model);
		public Task DeleteCommentAsync(Guid id);
	}
}
