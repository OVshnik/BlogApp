using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.ViewModels.Articles;
using BlogApp.ViewModels.Tags;
using BlogApp.ViewModels.Comments;
using Microsoft.AspNetCore.Identity;
using System.Runtime.Intrinsics.Arm;
using System.Security.Claims;
using BlogApp.Extensions;
using BlogApp.Exceptions;

namespace BlogApp.Services
{
	public class ArticleService : IArticleService
	{
		private readonly IArticleRepository _articleRepository;
		private readonly IMapper _mapper;
		private readonly UserManager<User> _userManager;
		private readonly ITagRepository _tagRepository;
		private readonly ICommentRepository _commentRepository;

		public ArticleService(IArticleRepository articleRepository, IMapper mapper, UserManager<User> userManager, ITagRepository tagRepository, ICommentRepository commentRepository)
		{
			_mapper = mapper;
			_articleRepository = articleRepository;
			_userManager = userManager;
			_tagRepository = tagRepository;
			_commentRepository = commentRepository;
		}
		/// <summary>
		/// Метод для создания модели представления новой статьи
		/// </summary>
		public async Task<CreateArticleViewModel> CreateNewArticleAsync(ClaimsPrincipal claims)
		{
			var user = await _userManager.GetUserAsync(claims);

			if(user != null)
			{
				var newArticle = new CreateArticleViewModel()
				{
					AuthorId = user.Id
				};
				var tags = await _tagRepository.GetAllTagsAsync();

				foreach (var tag in tags)
				{
					newArticle.Tags.Add(_mapper.Map(tag, new TagViewModel()));
				}
			}
			throw new ModelNotFoundException($"Пользователя с именем {claims.Identity?.Name} не удалось получить из БД");
		}
		/// <summary>
		/// Метод для создания новой статьи
		/// </summary>
		public async Task<Guid> CreateNewArticleAsync(CreateArticleViewModel model)
		{
			var tags = new List<Tag> { };
			if (model.Tags.Count != 0)
			{
				var checkTags = model.Tags.Where(x => x.IsChecked == true).ToList();
				var tagsId = checkTags.Select(x => x.Id).ToList();
				tags = _tagRepository.GetAllTagsAsync().Result.Where(x => tagsId.Contains(x.Id)).ToList();
			}
			var article = _mapper.Map<Article>(model);

			article.Tags = tags;

			var user = await _userManager.FindByIdAsync(model.AuthorId);
			if(user != null)
			{
				user.Articles.Add(article);

				await _articleRepository.CreateArticleAsync(article);
				await _userManager.UpdateAsync(user);
			}
			return article.Id;
		}
		/// <summary>
		/// Метод для получения статьи по id
		/// </summary>
		public async Task<ArticleViewModel> GetArticleAsync(Guid id)
		{
			var findArticle = await _articleRepository.GetArticleAsync(id);

			if (findArticle != null)
			{
				findArticle.ViewCounter++;

				await _articleRepository.UpdateArticleAsync(findArticle);

				var article = _mapper.Map<ArticleViewModel>(findArticle);

				article.Author=await _userManager.FindByIdAsync(article.AuthorId);

				foreach(var item in article.Comments)
				{
					item.CommentMaker=await _userManager.FindByIdAsync(item.CommentMakerId);
				}
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
		/// Метод для редактирования статьи
		/// </summary>
		public async Task<EditArticleViewModel> EditArticleAsync(Guid id)
		{
			var article = await _articleRepository.GetArticleAsync(id);
			if (article != null)
			{
				var model = _mapper.Map<EditArticleViewModel>(article);
				foreach (var tag in model.Tags)
				{
					tag.IsChecked = true;
				}
				var tags = await _tagRepository.GetAllTagsAsync();

				foreach (var t in tags)
				{
					var tag = new TagViewModel()
					{
						Id = t.Id,
						Name = t.Name,
						IsChecked = article.Tags.Contains(t) ? true : false,
					};
					if (!article.Tags.Contains(t))
					{
						model.Tags.Add(tag);
					}
				}
			}
			throw new ModelNotFoundException($"Статью с id={id} не удалось получить из БД");
		}
		/// <summary>
		/// Метод для обновления данных статьи в БД
		/// </summary>
		public async Task UpdateArticleAsync(EditArticleViewModel model)
		{
			var article = await _articleRepository.GetArticleAsync(model.Id);
			if (model != null && article != null)
			{
				article.Convert(model);

				foreach (var t in model.Tags)
				{
					var tag = await _tagRepository.GetTagAsync(t.Id);
					if (tag != null)
					{
						if (t.IsChecked)
						{
							article.Tags.Add(tag);
						}
						else
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
