using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.Exceptions;
using BlogApp.ViewModels.Comments;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace BlogApp.Services;

public class CommentService : ICommentService
{
	private readonly ICommentRepository _commentRepository;
	private readonly IArticleRepository _articleRepository;
	private readonly IMapper _mapper;
	private readonly UserManager<User> _userManager;

	public CommentService(ICommentRepository commentRepository, IArticleRepository articleRepository, IMapper mapper, UserManager<User> userManager)
	{
		_commentRepository = commentRepository;
		_articleRepository = articleRepository;
		_mapper = mapper;
		_userManager = userManager;
	}
	/// <summary>
	/// Метод для создания комментария
	/// </summary>
	public async Task<Guid> CreateNewCommentAsync(ClaimsPrincipal claims, CreateCommentViewModel model)
	{
		var currentUser = await _userManager.GetUserAsync(claims);
		var newComment = _mapper.Map<Comment>(model);
		if (currentUser != null)
		{
			newComment.CommentMakerId = currentUser.Id;

			var article = await _articleRepository.GetArticleAsync(model.ArticleId);

			//newComment.Article = article;

			if(article != null)
			newComment.ArticleId = article.Id;

			await _commentRepository.CreateCommentAsync(newComment);

			currentUser.Comments.Add(newComment);
			await _userManager.UpdateAsync(currentUser);
		}
		return newComment.Id;
	}
	/// <summary>
	/// Метод для получения комментария по id
	/// </summary>
	public async Task<CommentViewModel> GetCommentAsync(Guid id)
	{
		var comment = await _commentRepository.GetCommentAsync(id);
		if(comment != null)
		{
			var model = new CommentViewModel(comment);

			return model;
		}
		throw new ModelNotFoundException($"Комментарий с id={id} не удалось получить из БД");
	}
	/// <summary>
	/// Метод для получения всех комментариев
	/// </summary>
	public async Task<CommentListViewModel> GetAllCommentsAsync()
	{
		var comments = await _commentRepository.GetAllCommentsAsync();
		var articles = await _articleRepository.GetAllArticlesAsync();

		var model = new CommentListViewModel();

		if (comments != null && articles != null)
		{
			var sortComments = comments.OrderBy(x => x.Article?.Title).ThenBy(c => c.Created);
			model.Comments.AddRange(sortComments);

			model.Articles.AddRange(articles.OrderBy(x => x.Title));

			return model;
		}
		return model;
	}
	/// <summary>
	/// Метод для редактирования комментария
	/// </summary>
	public async Task<EditCommentViewModel> EditComment(Guid id)
	{
		var comment = await _commentRepository.GetCommentAsync(id);

		var model = _mapper.Map<EditCommentViewModel>(comment);

		return model;
	}
	/// <summary>
	/// Метод для обновления комментария в БД
	/// </summary>
	public async Task UpdateCommentAsync(EditCommentViewModel model)
	{
		var comment = await _commentRepository.GetCommentAsync(model.Id);
		if (comment != null)
		{
			if (!string.IsNullOrEmpty(model.Content))
				comment.Content = model.Content;
			await _commentRepository.UpdateCommentAsync(comment);
		}
	}
	/// <summary>
	/// Метод для удаления комментария из БД
	/// </summary>
	public async Task DeleteCommentAsync(Guid id)
	{
		await _commentRepository.DeleteCommentAsync(id);
	}
}
