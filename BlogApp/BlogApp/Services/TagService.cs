using BlogApp.Data.Models;
using BlogApp.Data.Repository;
using BlogApp.Data.UOW;

namespace BlogApp.Services
{
	public class TagService
	{
		private TagRepository _tagRepository;

		public TagService(IUnitOfWork unitOfWork)
		{
			_tagRepository = (TagRepository)unitOfWork.GetRepository<Tag>();
		}
		public async Task CreateNewTagAsync(Tag newTag)
		{
			await _tagRepository.AddTagAsync(newTag);
		}
		public async Task<Tag> GetTagByIdAsync(Guid id)
		{
			return await _tagRepository.GetTagByIdAsync(id);
		}
		public async Task<List<Tag>> GetAllTagsAsync()
		{
			return await _tagRepository.GetAllTags();
		}
		public async Task<List<Tag>> GetAllTagByArticleIdAsync(Article article)
		{
			return await _tagRepository.GetAllTagsByArticleAsync(article);
		}
		public async Task UpdateTagAsync(Tag updTag)
		{
			await _tagRepository.UpdateTag(updTag);
		}
		public async Task DeleteTagAsync(Tag tag)
		{
			await _tagRepository.DeleteTagAsync(tag);
		}
	}
}
