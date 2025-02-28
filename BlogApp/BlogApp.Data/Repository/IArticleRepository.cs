using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Repository
{
	public interface IArticleRepository
	{
		Task CreateArticleAsync(Article article);
		Task<List<Article>> GetAllArticlesAsync();
		Task<Article?> GetArticleAsync(Guid id);
		Task UpdateArticleAsync(Article article);
		Task DeleteArticleAsync(Guid id);
	}
}
