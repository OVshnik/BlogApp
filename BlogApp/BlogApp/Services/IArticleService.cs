using BlogApp.Data.Models;
using BlogApp.ViewModels.Articles;
using BlogApp.ViewModels.Tags;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.Services;

public interface IArticleService
{
	public Task<Guid> CreateNewArticleAsync(CreateArticleViewModel model);
	public Task<CreateArticleViewModel> CreateNewArticleAsync(ClaimsPrincipal claims);
	public Task<ArticleViewModel> GetArticleAsync(Guid id);
	public Task<ArticleListViewModel> GetAllArticlesAsync();
	public Task<EditArticleViewModel> EditArticleAsync(Guid id);
	public Task UpdateArticleAsync(EditArticleViewModel model);
	public Task DeleteArticleAsync(Guid id);
}
