﻿using BlogApp.Data.Models;
using BlogApp.API.ViewModels.Articles;
using BlogApp.API.ViewModels.Users;

namespace BlogApp.API.Extensions;

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
