using BlogApp.API.ViewModels.Articles;
using BlogApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.API.Services;

public interface IArticleService
{
	public Task CreateNewArticleAsync(CreateArticleViewModel model);
	public Task<ArticleViewModel> GetArticleAsync(Guid id);
	public Task<ArticleListViewModel> GetAllArticlesAsync();
	public Task UpdateArticleAsync(EditArticleViewModel model);
	public Task DeleteArticleAsync(Guid id);
}
