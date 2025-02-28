using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Repository
{
	public interface ICommentRepository
	{
		Task CreateCommentAsync(Comment comment);
		Task<List<Comment>> GetAllCommentsAsync();
		Task<Comment?> GetCommentAsync(Guid id);
		Task UpdateCommentAsync(Comment comment);
		Task DeleteCommentAsync(Guid id);
	}
}
