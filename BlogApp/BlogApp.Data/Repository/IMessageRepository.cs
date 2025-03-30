using BlogApp.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogApp.Data.Repository
{
	public interface IMessageRepository
	{
		Task<List<Message>> GetMessagesAsync(User sender, User recipient);
		Task AddNewMessageAsync(Message message);
	}
}
