using BlogApp.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Repository
{
	public class TagRepository : ITagRepository
	{
		private ApplicationDbContext _context;
		public TagRepository(ApplicationDbContext db)
		{
			_context = db;
		}
		public async Task CreateTagAsync(Tag tag)
		{
			_context.Tags.Add(tag);
			await _context.SaveChangesAsync();
		}
		public async Task<Tag?> GetTagAsync(Guid id)
		{
			return await _context.Tags.Include(x=>x.Articles).FirstOrDefaultAsync(x => x.Id == id);
		}
		public async Task<List<Tag>> GetAllTagsAsync()
		{
			return await _context.Tags.Include(x => x.Articles).ToListAsync();
		}
		public async Task UpdateTagAsync(Tag updTag)
		{
			_context.Update(updTag);
			await _context.SaveChangesAsync();
		}
		public async Task DeleteTagAsync(Guid id)
		{
			var tag = await GetTagAsync(id);
			if (tag != null)
			{
				_context.Remove(tag);
			}
			await _context.SaveChangesAsync();
		}
	}
}
