using AutoMapper;
using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.API.ViewModels.Tags;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using BlogApp.API.Exceptions;

namespace BlogApp.API.Services
{
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
		public async Task CreateNewTagAsync(CreateTagViewModel model)
		{
			var newTag = _mapper.Map<Tag>(model);

			await _tagRepository.CreateTagAsync(newTag);
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
				tags.Tags.AddRange(findTags);
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
}
