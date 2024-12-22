using BlogApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlogApp.Data.Repository
{
	public class TagRepository : Repository<Tag>
	{
		public TagRepository(ApplicationDbContext context) : base(context)
		{ }
		public async Task AddTagAsync(Tag tag)
		{
			await Create(tag);
		}
		public async Task<Tag> GetTagByIdAsync(Guid id)
		{
			return await Get(id);
		}
		public async Task<List<Tag>> GetAllTagsByArticleAsync(Article article)
		{
			return await Set.Include(x => x.Articles).AsQueryable().Where(x => x.Id == article.Id).ToListAsync();
		}
		public async Task<List<Tag>> GetAllTags()
		{
			return await GetAll();
		}
		public async Task UpdateTag(Tag updTag)
		{
			var tag = Set.AsEnumerable().Where(x => x.Id == updTag.Id).FirstOrDefault();
			if (tag != null)
				await Update(tag);
		}
		public async Task DeleteTagAsync(Tag delTag)
		{
			var tag = Set.AsEnumerable().FirstOrDefault(x => x.Id == delTag.Id);
			if (tag != null)
				await Delete(tag);
		}
	}
}
