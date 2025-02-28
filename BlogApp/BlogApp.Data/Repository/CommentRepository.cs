using BlogApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Repository
{
	public class CommentRepository : ICommentRepository
	{
		private ApplicationDbContext _context;
		public CommentRepository(ApplicationDbContext db)
		{
			_context = db;
		}
		public async Task CreateCommentAsync(Comment comment)
		{
			_context.Comments.Add(comment);
			await _context.SaveChangesAsync();
		}
		public async Task<Comment?> GetCommentAsync(Guid id)
		{
			return await _context.Comments.Include(x=>x.Article).Include(x => x.CommentMaker).FirstOrDefaultAsync(x=>x.Id==id);
		}
		public async Task<List<Comment>> GetAllCommentsAsync()
		{
			return await _context.Comments.Include(x=>x.Article).Include(x => x.CommentMaker).ToListAsync();
		}
		public async Task UpdateCommentAsync(Comment updComment)
		{
			_context.Update(updComment);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteCommentAsync(Guid id)
		{
			var comment = await GetCommentAsync(id);
			if (comment != null)
			{
				_context.Remove(comment);
			}
			await _context.SaveChangesAsync();
		}
	}
}
