using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.API.Extensions;
using BlogApp.API.ViewModels.Articles;
using Microsoft.AspNetCore.Identity;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using AutoMapper;
using System.Linq;
using BlogApp.API.ViewModels.Tags;
using BlogApp.API.Exceptions;

namespace BlogApp.API.Services
{
	public class ArticleService : IArticleService
	{
		private readonly IArticleRepository _articleRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly ITagRepository _tagRepository;

		public ArticleService(IArticleRepository articleRepository, IMapper mapper, UserManager<User> userManager, ITagRepository tagRepository)
		{
			_mapper = mapper;
			_articleRepository = articleRepository;
			_userManager = userManager;
			_tagRepository = tagRepository;
		}
		/// <summary>
		/// Метод для создания новой статьи
		/// </summary>
		public async Task CreateNewArticleAsync(CreateArticleViewModel model)
		{
			var article = _mapper.Map<Article>(model);

			foreach (var t in model.Tags)
			{
				var tag = await _tagRepository.GetTagAsync(t.Id);
				if (tag != null)
					article.Tags.Add(tag);
			}

			var user = await _userManager.FindByIdAsync(model.AuthorId);
			if (user != null)
			{
				user.Articles.Add(article);

				await _articleRepository.CreateArticleAsync(article);
				await _userManager.UpdateAsync(user);
			}
			throw new ModelNotFoundException($"Пользователя с id={model.AuthorId} не удалось получить из БД");
		}
		/// <summary>
		/// Метод для получения статьи по id
		/// </summary>
		public async Task<ArticleViewModel> GetArticleAsync(Guid id)
		{
			var findArticle = await _articleRepository.GetArticleAsync(id);

			if (findArticle != null)
			{
				var article = _mapper.Map<ArticleViewModel>(findArticle);

				article.Comments.AddRange(findArticle.Comments);

				return article;
			}
			throw new ModelNotFoundException($"Статью с id={id} не удалось получить из БД");
		}
		/// <summary>
		/// Метод для получения всех статей из БД и передачи модели представления
		/// </summary>
		public async Task<ArticleListViewModel> GetAllArticlesAsync()
		{
			var articles = await _articleRepository.GetAllArticlesAsync();

			var articleList = new ArticleListViewModel();
			if (articles != null)
			{
				articleList.Articles.AddRange(articles);
				return articleList;
			}
			return articleList;
		}
		/// <summary>
		/// Метод для обновления данных статьи в БД
		/// </summary>
		public async Task UpdateArticleAsync(EditArticleViewModel model)
		{
			var article = await _articleRepository.GetArticleAsync(model.Id);
			if (article != null)
			{
				article.Convert(model);
				foreach (var t in model.Tags)
				{
					var tag = await _tagRepository.GetTagAsync(t.Id);
					if (tag != null)
					{
						if (!article.Tags.Contains(_mapper.Map<Tag>(t)))
						{
							article.Tags.Add(tag);
						}
					}
				}
				foreach (var t in article.Tags)
				{
					var tag = await _tagRepository.GetTagAsync(t.Id);
					if (tag != null)
					{
						if (!model.Tags.Contains(_mapper.Map<TagForArticleViewModel>(t)))
						{
							article.Tags.Remove(tag);
						}
					}
				}
				await _articleRepository.UpdateArticleAsync(article);
			}
		}
		/// <summary>
		/// Метод для удаления статьи из БД
		/// </summary>
		public async Task DeleteArticleAsync(Guid id)
		{
			await _articleRepository.DeleteArticleAsync(id);
		}
	}
}
