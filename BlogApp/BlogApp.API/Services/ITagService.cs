using BlogApp.Data.Models;
using BlogApp.API.ViewModels.Tags;

namespace BlogApp.API.Services;

public interface ITagService
{
	public Task CreateNewTagAsync(CreateTagViewModel model);
	public Task UpdateTagAsync(EditTagViewModel model);
	public Task<TagViewModel> GetTagAsync(Guid id);
	public Task<ListTagsViewModel> GetAllTagsAsync();
	public Task DeleteTagAsync(Guid id);
}

