using BlogApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Repository
{
	internal class MessageRepository : IMessageRepository
	{
		private ApplicationDbContext _context;
		public MessageRepository(ApplicationDbContext db)
		{
			_context = db;
		}
		public async Task<List<Message>> GetMessagesAsync(User sender, User recipient)
		{
			var from = await _context.Messages.Include(s => s.Sender).AsAsyncEnumerable().Where(x => x.SenderId == sender.Id && x.RecipientId == recipient.Id).ToListAsync();
			var to = await _context.Messages.Include(s => s.Recipient).AsAsyncEnumerable().Where(x => x.SenderId == recipient.Id && x.RecipientId == sender.Id).ToListAsync();


			var result = new List<Message>();
			result.AddRange(from);
			result.AddRange(to);
			result.OrderBy(x => x.Id);
			return result;
		}
		public async Task AddNewMessageAsync(Message message)
		{
			_context.Messages.Add(message);
			await _context.SaveChangesAsync();
		}
	}
}
