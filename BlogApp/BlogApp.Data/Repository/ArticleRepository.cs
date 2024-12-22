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
	public class ArticleRepository : Repository<Article>
	{
		public ArticleRepository(ApplicationDbContext db) : base(db)
		{ }
		public async Task AddArticleAsync(Article article)
		{
			await Create(article);
		}
		public async Task<Article> GetArticleByIdAsync(Guid id)
		{
				return await Get(id);
		}
		public async Task<List<Article>> GetAllArticles()
		{
			return await Set.AsQueryable().ToListAsync();
		}
		public async Task<List<Article>> GetAllArticlesByAuthorIdAsync(User user)
		{
			return await Set.AsQueryable().Where(x => x.AuthorId == user.Id).ToListAsync();

		}
		public async Task UpdateArticleAsync(Article updArticle)
		{
			var article = Set.AsEnumerable().Where(x=>x.Id== updArticle.Id).FirstOrDefault();
			if (article != null)
			await Update(article);
		}
		public async Task DeleteArticleAsync(Article delArticle)
		{
			var article = Set.AsEnumerable().FirstOrDefault(x => x.Id == delArticle.Id);
			if (article != null)
				await Delete(article);
		}
	}
}
