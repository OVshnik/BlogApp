using BlogApp.Data.Models;
using BlogApp.ViewModels.Tags;

namespace BlogApp.Services
{
	public interface ITagService
	{
		public Task<Guid> CreateNewTagAsync(CreateTagViewModel model);
		public Task<EditTagViewModel> EditTag(Guid id);
		public Task UpdateTagAsync(EditTagViewModel model);
		public Task<TagViewModel> GetTagAsync(Guid id);
		public Task<ListTagsViewModel> GetAllTagsAsync();
		public Task DeleteTagAsync(Guid id);
	}
}

