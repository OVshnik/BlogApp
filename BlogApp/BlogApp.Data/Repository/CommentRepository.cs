using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Repository
{
	public class CommentRepository:Repository<Comment>
	{
		public CommentRepository(ApplicationDbContext db) : base(db)
		{ }
		public async Task AddCommentAsync(Comment comment)
		{
			await Create(comment);
		}
		public async Task DeleteCommentAsync(Comment deleteComment)
		{
			var comment = Set.AsEnumerable().FirstOrDefault(x => x.Id == deleteComment.Id);
			if (comment != null)
				await Delete(comment);
		}
		public async Task<Comment> GetCommentByIdAsync(Guid id)
		{
			return await Get(id);
		}
		public async Task<List<Comment>> GetAllCommentsByUserAsync(User user)
		{
			return await Task.Run(() => Set.AsEnumerable().Where(x => x.CommentMakerId == user.Id).ToList());
		}

		public async Task<List<Comment>>GetAllCommentsByArticleAsync(Guid id)
		{
			return await Task.Run(() => Set.AsEnumerable().Where(x => x.ArticleId == id).ToList());
		}
		public async Task UpdateCommentAsync(Comment updComment)
		{
			var comment = Set.AsEnumerable().Where(x => x.Id == updComment.Id).FirstOrDefault();
			if (comment != null)
				await Update(comment);
		}
	}
}
