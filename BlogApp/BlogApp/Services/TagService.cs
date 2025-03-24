using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.Exceptions;
using BlogApp.ViewModels.Tags;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace BlogApp.Services;

public class TagService : ITagService
{
	private readonly IMapper _mapper;
	private readonly ITagRepository _tagRepository;
	private readonly IArticleRepository _articleRepository;

	public TagService(ITagRepository tagRepository, IMapper mapper, IArticleRepository articleRepository)
	{
		_tagRepository = tagRepository;
		_mapper = mapper;
		_articleRepository = articleRepository;
	}

	/// <summary>
	/// Метод для создания тега в БД
	/// </summary>
	public async Task<Guid> CreateNewTagAsync(CreateTagViewModel model)
	{
		var newTag = _mapper.Map<Tag>(model);

		await _tagRepository.CreateTagAsync(newTag);
		return newTag.Id;
	}

	/// <summary>
	/// Метод для редактирования тега в БД
	/// </summary>
	public async Task<EditTagViewModel> EditTag(Guid id)
	{
		var tag = await _tagRepository.GetTagAsync(id);
		var model = _mapper.Map<EditTagViewModel>(tag);

		return model;
	}

	/// <summary>
	/// Метод для обновления тега в БД
	/// </summary>
	public async Task UpdateTagAsync(EditTagViewModel model)
	{
		var tag = await _tagRepository.GetTagAsync(model.Id);
		if (tag != null)
		{
			if (!string.IsNullOrEmpty(model.Name))
				tag.Name = model.Name;
			await _tagRepository.UpdateTagAsync(tag);
		}
	}

	/// <summary>
	/// Метод для получения тега из БД
	/// </summary>
	public async Task<TagViewModel> GetTagAsync(Guid id)
	{
		var findTag = await _tagRepository.GetTagAsync(id);
		if (findTag != null)
		{
			var tag = _mapper.Map<TagViewModel>(findTag);
			return tag;
		}
		throw new ModelNotFoundException($"Тег с id={id} не удалось получить из БД");
	}

	/// <summary>
	/// Метод для получения всех тегов из БД
	/// </summary>
	public async Task<ListTagsViewModel> GetAllTagsAsync()
	{
		var findTags = await _tagRepository.GetAllTagsAsync();
		var tags = new ListTagsViewModel();
		if (findTags != null)
		{
			foreach (var tag in findTags)
			{
				var model = _mapper.Map<TagViewModel>(tag);
				tags.Tags.Add(model);
			}
			return tags;
		}
		return tags;
	}

	/// <summary>
	/// Метод для удаления тега из БД
	/// </summary>
	public async Task DeleteTagAsync(Guid id)
	{
		await _tagRepository.DeleteTagAsync(id);
	}
}
