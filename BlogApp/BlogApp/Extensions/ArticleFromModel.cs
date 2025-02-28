using BlogApp.Data.Models;
using BlogApp.ViewModels.Articles;
using BlogApp.ViewModels.Users;

namespace BlogApp.Extensions
{
    public static class ArticleFromModel
	{
		public static Article Convert(this Article article, EditArticleViewModel viewModel)
		{
			if (!string.IsNullOrEmpty(viewModel.Title))
			{
				article.Title = viewModel.Title;
			}
			if (!string.IsNullOrEmpty(viewModel.Content))
			{
				article.Content = viewModel.Content;
			}
			if (!string.IsNullOrEmpty(viewModel.Description))
			{
				article.Description = viewModel.Description;
			}
			return article;
		}
	}
}
