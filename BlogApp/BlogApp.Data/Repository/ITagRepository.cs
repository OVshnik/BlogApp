using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Repository
{
	public interface ITagRepository
	{
		Task CreateTagAsync(Tag tag);
		Task<List<Tag>> GetAllTagsAsync();
		Task<Tag?> GetTagAsync(Guid id);
		Task UpdateTagAsync(Tag tag);
		Task DeleteTagAsync(Guid id);
	}
}
