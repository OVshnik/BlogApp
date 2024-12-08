using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.Data.UOW;
using System.Runtime.Intrinsics.Arm;

namespace BlogApp.Services
{
	public class ArticleService
	{
		private ArticleRepository _articleRepository;

		public ArticleService(IUnitOfWork unitOfWork) => _articleRepository = (ArticleRepository)unitOfWork.GetRepository<Article>();
		public async Task CreateNewArticleAsync(Article newArticle)
		{
			await _articleRepository.AddArticleAsync(newArticle);
		}
		public async Task<Article> GetArticleByIdAsync(Guid id)
		{
			return await _articleRepository.GetArticleByIdAsync(id);
		}
		public async Task<List<Article>> GetAllArticlesAsync()
		{
			return await _articleRepository.GetAllArticles();
		}
		public async Task<List<Article>> GetAllArticlesByAuthorIdAsync(User user)
		{
			return await _articleRepository.GetAllArticlesByAuthorIdAsync(user);
		}
		public async Task UpdateArticleAsync(Article article)
		{
			await _articleRepository.UpdateArticleAsync(article);
		}
		public async Task DeleteArticleAsync(Article article)
		{
			await _articleRepository.DeleteArticleAsync(article);
		}
}
}
