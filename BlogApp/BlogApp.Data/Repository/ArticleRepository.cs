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
	public class ArticleRepository : IArticleRepository
	{
		private ApplicationDbContext _context;
		public ArticleRepository(ApplicationDbContext db)
		{
			_context = db;
		}
		public async Task CreateArticleAsync(Article article)
		{
			_context.Articles.Add(article);
			await _context.SaveChangesAsync();
		}
		public async Task<Article?> GetArticleAsync(Guid id)
		{
			return await _context.Articles.Include(a => a.Tags).Include(a => a.Comments).Where(x=>x.Id==id).FirstOrDefaultAsync();
		}
		public async Task<List<Article>> GetAllArticlesAsync()
		{
			return await _context.Articles.Include(x => x.Tags).ToListAsync();
		}
		public async Task UpdateArticleAsync(Article updArticle)
		{
			_context.Update(updArticle);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteArticleAsync(Guid id)
		{
			var article = await GetArticleAsync(id);
			if (article != null)
			{
				_context.Remove(article);
			}
			await _context.SaveChangesAsync();
		}
	}
}
