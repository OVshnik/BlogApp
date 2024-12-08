using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.Data.UOW;

namespace BlogApp.Services
{
	public class CommentService
	{
		private CommentRepository _commentRepository;

		public CommentService(IUnitOfWork unitOfWork)
		{
			_commentRepository = (CommentRepository)unitOfWork.GetRepository<Comment>();
		}
		public async Task CreateNewCommentAsync(Comment comment)
		{
			await _commentRepository.AddCommentAsync(comment);
		}
		public async Task<List<Comment>> GetCommentByUserAsync(User user)
		{
			return await _commentRepository.GetAllCommentsByUserAsync(user);
		}
		public async Task<Comment> GetCommentByIdAsync(Guid id)
		{
			return await _commentRepository.GetCommentByIdAsync(id);
		}
		public async Task<List<Comment>> GetCommentsByArticleAsync(Guid id)
		{
			return await _commentRepository.GetAllCommentsByArticleAsync(id);
		}
		public async Task UpdateCommentAsync(Comment comment)
		{
			await _commentRepository.UpdateCommentAsync(comment);
		}
		public async Task DeleteCommentAsync(Comment comment)
		{
			await _commentRepository.DeleteCommentAsync(comment);
		}
	}
}
